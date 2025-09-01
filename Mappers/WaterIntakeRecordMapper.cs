using Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class WaterIntakeRecordMapper
    {
        public static WaterIntakeRecordDto ToWaterIntakeRecordDto(this WaterIntakeRecord waterIntakeRecord)
        {
            return new WaterIntakeRecordDto
            {
                Id = waterIntakeRecord.Id,
                AmountInMilliliters = waterIntakeRecord.AmountInMilliliters,
                Time = waterIntakeRecord.Time,
                ModifiedAt = waterIntakeRecord.ModifiedAt,
                Deleted = waterIntakeRecord.Deleted
            };
        }

        public static WaterIntakeRecord ToWaterIntakeRecord(this AddWaterIntakeRecordRequestDto requestDto, string userId)
        {
            return new WaterIntakeRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                AmountInMilliliters = requestDto.AmountInMilliliters,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static WaterIntakeRecord ToWaterIntakeRecord(this WaterIntakeRecordDto waterIntakeRecordDto, string userId)
        {
            return new WaterIntakeRecord
            {
                Id = waterIntakeRecordDto.Id,
                UserId = userId,
                AmountInMilliliters = waterIntakeRecordDto.AmountInMilliliters,
                Time = waterIntakeRecordDto.Time,
                ModifiedAt = waterIntakeRecordDto.ModifiedAt,
                Deleted = waterIntakeRecordDto.Deleted
            };
        }

        public static WaterIntakeRecord ToWaterIntakeRecord(this UpdateWaterIntakeRecordRequestDto requestDto, string userId)
        {
            return new WaterIntakeRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                AmountInMilliliters = requestDto.AmountInMilliliters,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncWaterIntakeRecordResponseDto ToSyncWaterIntakeRecordResponseDto(
            this IEnumerable<WaterIntakeRecord> waterIntakeRecords,
            DateTime syncedAt
        )
        {
            return new SyncWaterIntakeRecordResponseDto
            {
                LastSyncedAt = syncedAt,
                WaterIntakeRecords = waterIntakeRecords.Select(wir => wir.ToWaterIntakeRecordDto()).ToList()
            };
        }
    }
}
