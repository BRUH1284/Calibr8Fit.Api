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
            syncService,
            UserActivityMapper.ToUserActivityDto,
            UserActivityMapper.ToUserActivity,
            UserActivityMapper.ToUserActivity,
            UserActivityMapper.ToSyncUserActivityResponseDto
        )
    { }
}