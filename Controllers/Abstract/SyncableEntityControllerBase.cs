using Calibr8Fit.Api.Interfaces.DataTransferObjects;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers.Abstract
{
    [ApiController]
    [Authorize]
    public abstract class SyncableEntityControllerBase<
        TEntity,
        TDto,
        TKey,
        TRepository,
        TUpdateDto,
        TAddDto,
        TSyncRequestDto,
        TSyncResponseDto
    >(
        ICurrentUserService currentUserService,
        TRepository repository,
        ISyncService<TEntity, TKey> syncService,
        Func<TEntity, TDto> entityToDto,
        Func<TUpdateDto, string, TEntity> updateDtoToEntity,
        Func<TAddDto, string, TEntity> addDtoToEntity,
        Func<List<TEntity>, DateTime, TSyncResponseDto> toSyncResponseDtoFunc
    ) : AppControllerBase(currentUserService)
        where TEntity : class, ISyncableUserEntity<TKey>
        where TKey : notnull
        where TRepository : IUserSyncRepositoryBase<TEntity, TKey>
        where TSyncRequestDto : ISyncRequestDto<TAddDto>
        where TSyncResponseDto : ISyncResponseDto<TDto>
    {
        protected readonly TRepository _repository = repository;
        protected readonly ISyncService<TEntity, TKey> _syncService = syncService;

        [HttpGet("last-updated-at")]
        public virtual Task<IActionResult> GetLastUpdatedAt() =>
            WithUserId(async userId => Ok(await _syncService.GetLastSyncedAtAsync(userId)));

        [HttpGet]
        public virtual Task<IActionResult> GetAll() =>
            WithUserId(async userId =>
            {
                // Get all entities for the user
                var entities = await _repository.GetAllByUserIdAsync(userId);
                var entitiesDtos = entities.Select(entityToDto);

                return Ok(entitiesDtos);
            });

        [HttpGet("{id}")]
        public virtual Task<IActionResult> GetById(TKey id) =>
            WithUserId(async userId =>
            {
                // Get entity by id
                var entity = await _repository.GetByUserIdAndKeyAsync(userId, id);

                // If entity is null, return NotFound
                return entity is null
                    ? NotFound($"Entity with id: {id} not found.")
                    : Ok(entityToDto(entity));
            });

        [HttpPost]

        public virtual Task<IActionResult> Add([FromBody] TAddDto requestDto) =>
        WithUserId(async userId =>
        {
            // Add new entity to DB
            var addedEntity = await _repository
                .AddAsync(addDtoToEntity(requestDto, userId));

            // If record is null, return BadRequest
            return addedEntity is null
                ? BadRequest("Failed to add entity.")
                : CreatedAtAction(
                    nameof(GetById),
                    new { id = addedEntity.Id },
                    entityToDto(addedEntity)
                );
        });

        [HttpPut("{id}")]
        public virtual Task<IActionResult> Update(Guid id, [FromBody] TUpdateDto requestDto) =>
        WithUserId(async userId =>
        {
            // Update existing entity
            var updatedEntity = await _repository
                .UpdateByUserIdAsync(userId, updateDtoToEntity(requestDto, userId));

            // If record is null, return NotFound
            return updatedEntity is null
                ? NotFound($"Entity with id: {id} not found.")
                : Ok(entityToDto(updatedEntity));
        });

        [HttpDelete("{id}")]
        public virtual Task<IActionResult> Delete(TKey id) =>
        WithUserId(async userId =>
        {
            // Delete entity record by id
            var deleted = await _repository
                .MarkAsDeletedByUserIdAsync(userId, id);

            // If no entity was deleted, return NotFound
            return deleted is null
                ? NotFound($"Entity with id: {id} not found.")
                : NoContent();
        });

        [HttpPost("sync")]
        public virtual Task<IActionResult> Sync([FromBody] TSyncRequestDto requestDto) =>
        WithUserId(async userId =>
        {
            // Get synced entities from request DTOs
            var result = await _syncService.Sync(
                userId,
                requestDto.AddEntityRequestDtos.Select(e => addDtoToEntity(e, userId)),
                requestDto.LastSyncedAt
            );

            return Ok(toSyncResponseDtoFunc(result, DateTime.UtcNow));
        });
    }
}