using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Repository.Abstract;

namespace Calibr8Fit.Api.Repository
{
    public class ActivityRecordRepository(
        ApplicationDbContext context
    ) : UserSyncRepositoryBase<ActivityRecord, Guid>(context), IActivityRecordRepository
    {

    }
}