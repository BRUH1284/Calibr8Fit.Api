using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.Food;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController(
        IFoodRepository foodRepository
        ) : EntityControllerBase<
        Food,
        FoodDto,
        Guid,
        IFoodRepository,
        UpdateFoodRequestDto,
        AddFoodRequestDto
        >(
            foodRepository,
            FoodMapper.ToFoodDto,
            FoodMapper.ToFood,
            FoodMapper.ToFood
    )
    { }
}