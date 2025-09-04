using Calibr8Fit.Api.DataTransferObjects.WeightRecord;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class WeightRecordMapper
    {
        public static WeightRecordDto ToWeightRecordDto(this WeightRecord weightRecord)
        {
            return new WeightRecordDto
            {
                Id = weightRecord.Id,
                Weight = weightRecord.Weight,
                Time = weightRecord.Time,
                ModifiedAt = weightRecord.ModifiedAt,
                Deleted = weightRecord.Deleted
            };
        }

        public static WeightRecord ToWeightRecord(this AddWeightRecordRequestDto requestDto, string userId)
        {
            return new WeightRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                Weight = requestDto.Weight,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static WeightRecord ToWeightRecord(this WeightRecordDto weightRecordDto, string userId)
        {
            return new WeightRecord
            {
                Id = weightRecordDto.Id,
                UserId = userId,
                Weight = weightRecordDto.Weight,
                Time = weightRecordDto.Time,
                ModifiedAt = weightRecordDto.ModifiedAt,
                Deleted = weightRecordDto.Deleted
            };
        }

        public static WeightRecord ToWeightRecord(this UpdateWeightRecordRequestDto requestDto, string userId)
        {
            return new WeightRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                Weight = requestDto.Weight,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncWeightRecordResponseDto ToSyncWeightRecordResponseDto(
            this IEnumerable<WeightRecord> weightRecords,
            DateTime syncedAt
        )
        {
            return new SyncWeightRecordResponseDto
            {
                LastSyncedAt = syncedAt,
                WeightRecords = weightRecords.Select(wr => wr.ToWeightRecordDto()).ToList()
            };
        }
    }
}
