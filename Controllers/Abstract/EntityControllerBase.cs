using Calibr8Fit.Api.Interfaces.DataTransferObjects;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers.Abstract
{
    [ApiController]
    public abstract class EntityControllerBase<
        TEntity,
        TDto,
        TKey,
        TRepository,
        TUpdateDto,
        TAddDto
    >(
        TRepository repository,
        Func<TEntity, TDto> entityToDtoFunc,
        Func<TUpdateDto, TEntity> updateDtoToEntityFunc,
        Func<TAddDto, TEntity> addDtoToEntityFunc
    ) : ControllerBase
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
        where TRepository : IRepositoryBase<TEntity, TKey>, IDataVersionProvider
        where TUpdateDto : IUpdateRequestDto<TKey>
    {
        protected readonly TRepository _repository = repository;

        [HttpGet("last-updated-at")]
        public async Task<IActionResult> GetLastUpdatedAt() =>
            Ok(await _repository.LastUpdatedAtAsync());
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get all entities from DB
            var entities = await _repository.GetAllAsync();
            var entityDtos = entities.Select(entityToDtoFunc);

            return Ok(entityDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(TKey id)
        {
            // Get entity by id from DB
            var entity = await _repository.GetAsync(id);

            // If entity is null, return NotFound
            return entity is null
                ? NotFound($"Entity with id: {id} not found.")
                : Ok(entityToDtoFunc(entity));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] TAddDto requestDto)
        {
            // Add new entity to DB
            var entity = await _repository.AddAsync(addDtoToEntityFunc(requestDto));

            // If entity is null, return BadRequest
            return entity is null
                ? BadRequest("Failed to add entity.")
                : CreatedAtAction(
                    nameof(GetById),
                    new { id = entity.Id },
                    entityToDtoFunc(entity));
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] TUpdateDto updateDto)
        {
            var entity = await _repository.UpdateAsync(updateDtoToEntityFunc(updateDto));

            // If entity is null, return NotFound
            return entity is null
                ? NotFound($"Entity with id: {updateDto.Id} not found.")
                : Ok(entityToDtoFunc(entity));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(TKey id)
        {
            var entity = await _repository.DeleteAsync(id);

            // If entity is null, return NotFound
            return entity is null
                ? NotFound($"Entity with id: {id} not found.")
                : NoContent();
        }
    }
}