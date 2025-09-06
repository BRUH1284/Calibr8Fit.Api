using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Repository.Base;
using Calibr8Fit.Api.Services;

namespace Calibr8Fit.Api.Extensions
{
    public static class RepoRegistration
    {
        public static IServiceCollection AddDataVersionRepo<T, TKey>(
            this IServiceCollection services,
            DataResource dataResource
            )
            where T : class, IEntity<TKey>
            where TKey : notnull
        {
            services.AddScoped<IDataVersionRepositoryBase<T, TKey>, DataVersionRepositoryBase<T, TKey>>(sp =>
                new DataVersionRepositoryBase<T, TKey>(
                    sp.GetRequiredService<ApplicationDbContext>(),
                    sp.GetRequiredService<IDataVersionRepository>(),
                    dataResource
                ));
            return services;
        }
        public static IServiceCollection AddUserSyncRepo<T, TKey>(
            this IServiceCollection services
            )
            where T : class, ISyncableUserEntity<TKey>
            where TKey : notnull
        {
            services.AddScoped<ISyncService<T, TKey>, SyncService<T, TKey>>();

            services.AddScoped<IUserRepositoryBase<T, TKey>, UserRepositoryBase<T, TKey>>();
            services.AddScoped<IUserSyncRepositoryBase<T, TKey>, UserSyncRepositoryBase<T, TKey>>();
            return services;
        }
    }
}