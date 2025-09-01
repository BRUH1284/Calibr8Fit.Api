using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/activity")]
    [ApiController]
    public class ActivityController(IActivityRepository activityRepository) : ControllerBase
    {
        private readonly IActivityRepository _activityRepository = activityRepository;

        [HttpGet("last-updated-at")]
        public async Task<IActionResult> GetLastUpdatedAt()
        {
            var lastUpdatedAt = await _activityRepository.LastUpdatedAtAsync();

            // If no last updated time, return NoContent
            return lastUpdatedAt is null
                ? NoContent()
                : Ok(lastUpdatedAt);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            // Get all activities from DB
            var activities = await _activityRepository.GetAllAsync();
            var activityDtos = activities.Select(a => a.ToActivityDto());

            return Ok(activityDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            // Get activity by id from DB
            var activity = await _activityRepository.GetAsync(id);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with id: {id} not found.")
                : Ok(activity.ToActivityDto());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddActivity([FromBody] AddActivityRequestDto activityDto)
        {
            // Add new activity to DB
            var activity = await _activityRepository.AddAsync(activityDto.ToActivity());

            // If activity is null, return BadRequest
            return activity is null
                ? BadRequest("Failed to add activity.")
                : CreatedAtAction(
                    nameof(GetActivityById),
                    new { id = activity.Id },
                    activity.ToActivityDto());
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityRequestDto updateDto)
        {
            var activity = await _activityRepository.UpdateAsync(updateDto.ToActivity());

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with id: {updateDto.Id} not found.")
                : Ok(activity.ToActivityDto());
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var activity = await _activityRepository.DeleteAsync(id);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with id: {id} not found.")
                : NoContent();
        }
    }
}