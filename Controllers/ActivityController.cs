using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/activity")]
    [ApiController]
    public class ActivityController(
        IActivityRepository activityRepository
        ) : EntityControllerBase<
        Activity,
        ActivityDto,
        Guid,
        IActivityRepository,
        UpdateActivityRequestDto,
        AddActivityRequestDto
        >(
            activityRepository,
            ActivityMapper.ToActivityDto,
            ActivityMapper.ToActivity,
            ActivityMapper.ToActivity
        )
    { }
}