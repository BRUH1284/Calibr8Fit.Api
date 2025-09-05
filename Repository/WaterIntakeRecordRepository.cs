using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Repository.Abstract;

namespace Calibr8Fit.Api.Repository
{
    public class WaterIntakeRecordRepository(
        ApplicationDbContext context
    ) : UserSyncRepositoryBase<WaterIntakeRecord, Guid>(context), IWaterIntakeRecordRepository
    {

    }
}