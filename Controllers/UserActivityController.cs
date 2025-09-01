using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.UserActivity;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/activity/my")]
    [ApiController]
    [Authorize]
    public class UserActivityController(
        ICurrentUserService currentUserService,
        IUserActivityRepository userActivityRepository,
        ISyncService<UserActivity, Guid> syncService
        ) : SyncableEntityControllerBase<
            UserActivity,
            UserActivityDto,
            Guid,
            IUserActivityRepository,
            UpdateUserActivityRequestDto,
            AddUserActivityRequestDto,
            SyncUserActivitiesRequestDto,
            SyncUserActivityResponseDto
        >(
            currentUserService,
            userActivityRepository,
            syncService
        )
    {
        protected override UserActivityDto ToDto(UserActivity entity) => entity.ToUserActivityDto();
        protected override UserActivity ToEntity(UpdateUserActivityRequestDto updateDto, string userId) =>
            updateDto.ToUserActivity(userId);
        protected override UserActivity ToEntity(AddUserActivityRequestDto addDto, string userId) =>
            addDto.ToUserActivity(userId);
        protected override SyncUserActivityResponseDto ToSyncResponseDto(DateTime lastSyncedAt, List<UserActivity> entities) =>
           entities.ToSyncUserActivityResponseDto(lastSyncedAt);
    }
}