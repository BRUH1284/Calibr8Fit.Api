using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/water-intake")]
    [ApiController]
    [Authorize]
    public class WaterIntakeController(
        IWaterIntakeRecordRepository waterIntakeRecordRepository,
        ICurrentUserService currentUserService,
        ISyncService<WaterIntakeRecord, Guid> syncService
        ) : SyncableEntityControllerBase<
        WaterIntakeRecord,
        WaterIntakeRecordDto,
        Guid,
        IWaterIntakeRecordRepository,
        UpdateWaterIntakeRecordRequestDto,
        AddWaterIntakeRecordRequestDto,
        SyncWaterIntakeRecordRequestDto,
        SyncWaterIntakeRecordResponseDto
        >(
            currentUserService,
            waterIntakeRecordRepository,
            syncService,
            WaterIntakeRecordMapper.ToWaterIntakeRecordDto,
            WaterIntakeRecordMapper.ToWaterIntakeRecord,
            WaterIntakeRecordMapper.ToWaterIntakeRecord,
            WaterIntakeRecordMapper.ToSyncWaterIntakeRecordResponseDto
        )
    { }
}