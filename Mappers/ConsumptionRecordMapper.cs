using Calibr8Fit.Api.DataTransferObjects.ConsumptionRecord;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class ConsumptionRecordMapper
    {
        public static ConsumptionRecordDto ToConsumptionRecordDto(this ConsumptionRecord consumptionRecord)
        {
            return new ConsumptionRecordDto
            {
                Id = consumptionRecord.Id,
                FoodId = consumptionRecord.FoodId,
                UserMealId = consumptionRecord.UserMealId,
                Quantity = consumptionRecord.Quantity,
                Time = consumptionRecord.Time,
                ModifiedAt = consumptionRecord.ModifiedAt,
                Deleted = consumptionRecord.Deleted
            };
        }

        public static ConsumptionRecord ToConsumptionRecord(this AddConsumptionRecordRequestDto requestDto, string userId)
        {
            return new ConsumptionRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                FoodId = requestDto.FoodId,
                UserMealId = requestDto.UserMealId,
                Quantity = requestDto.Quantity,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static ConsumptionRecord ToConsumptionRecord(this ConsumptionRecordDto consumptionRecordDto, string userId)
        {
            return new ConsumptionRecord
            {
                Id = consumptionRecordDto.Id,
                UserId = userId,
                FoodId = consumptionRecordDto.FoodId,
                UserMealId = consumptionRecordDto.UserMealId,
                Quantity = consumptionRecordDto.Quantity,
                Time = consumptionRecordDto.Time,
                ModifiedAt = consumptionRecordDto.ModifiedAt,
                Deleted = consumptionRecordDto.Deleted
            };
        }

        public static ConsumptionRecord ToConsumptionRecord(this UpdateConsumptionRecordRequestDto requestDto, string userId)
        {
            return new ConsumptionRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                FoodId = requestDto.FoodId,
                UserMealId = requestDto.UserMealId,
                Quantity = requestDto.Quantity,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncConsumptionRecordResponseDto ToSyncConsumptionRecordResponseDto(
            this IEnumerable<ConsumptionRecord> consumptionRecords,
            DateTime syncedAt
        )
        {
            return new SyncConsumptionRecordResponseDto
            {
                LastSyncedAt = syncedAt,
                ConsumptionRecords = consumptionRecords.Select(cr => cr.ToConsumptionRecordDto()).ToList()
            };
        }
    }
}
