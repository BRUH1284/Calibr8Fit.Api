using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.UserFood;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/user-food")]
    [ApiController]
    public class UserFoodController(
        ICurrentUserService currentUserService,
        IUserFoodRepository userFoodRepository,
        ISyncService<UserFood, Guid> syncService
        ) : SyncableEntityControllerBase<
        UserFood,
        UserFoodDto,
        Guid,
        IUserFoodRepository,
        UpdateUserFoodRequestDto,
        AddUserFoodRequestDto,
        SyncUserFoodsRequestDto,
        SyncUserFoodResponseDto
        >(
            currentUserService,
            userFoodRepository,
            syncService,
            UserFoodMapper.ToUserFoodDto,
            UserFoodMapper.ToUserFood,
            UserFoodMapper.ToUserFood,
            UserFoodMapper.ToSyncUserFoodResponseDto
        )
    { }
}
