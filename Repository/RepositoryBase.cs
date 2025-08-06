using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public abstract class RepositoryBase<T, HT, TKey>(
        ApplicationDbContext context
        ) : IRepositoryBase<T, TKey>
        where T : class, IEntity<TKey>
        where HT : class, IEntity<TKey>
        where TKey : notnull
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();
        protected readonly DbSet<HT> _hDbSet = context.Set<HT>();

        public virtual async Task<T?> GetAsync(TKey key)
        {
            // Get entity by key
            return await _dbSet.FindAsync(key);
        }
        public virtual async Task<List<T>> GetRangeAsync(IEnumerable<TKey> keys)
        {
            // Get range of entities by ids
            return await _dbSet
                .Where(e => keys.Contains(e.Id))
                .ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            // Get all entities
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<bool> KeyExistsAsync(TKey key)
        {
            // Check if entity exists by key
            return await _dbSet.AnyAsync(e => e.Id.Equals(key));
        }
        public virtual async Task<T?> AddAsync(T entity)
        {
            // Check if entity already exists
            if (await KeyExistsInHierarchyAsync(entity.Id)) return null;

            // Add new entity to DB
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var existingKeys = await KeyRangeExistsInHierarchyAsync(entities.Select(e => e.Id));

            // Filter out existing entities
            var newEntities = entities
                .Where(e => !existingKeys.Contains(e.Id))
                .ToList();

            // If no new entities to add, return empty list
            if (newEntities.Count == 0) return [];

            // Add range of entities to DB
            await _dbSet.AddRangeAsync(newEntities);
            await SaveChangesAsync();
            return newEntities;
        }
        public virtual async Task<T?> AddOrUpdateAsync(T entity)
        {
            // Check if entity already exists
            var existing = await GetEntityAsync(entity);

            if (existing is not null)
            {
                // Update existing entity
                UpdateProperties(existing, entity);
                await SaveChangesAsync();
                return existing;
            }

            // If entity does not exist, check if it can be added
            if (await KeyExistsInHierarchyAsync(entity.Id)) return null;

            // Add new entity to DB
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            // Get existing entity by id
            var existing = await GetEntityAsync(entity);

            // If entity does not exist, return null
            if (existing is null) return null;

            // Update existing entity with new values
            UpdateProperties(existing, entity);

            // Save changes in DB
            await SaveChangesAsync();
            return existing;
        }

        public async Task<List<T>> UpdateRangeAsync(IEnumerable<T> updatedEntities)
        {
            // Get existing entities from DB
            var existingEntities = await GetEntityRangeAsync(updatedEntities);

            var updatedEntitiesDict = updatedEntities.ToDictionary(e => e.Id, e => e);
            // Update each existing entity with corresponding updated entity
            foreach (var existingEntity in existingEntities)
                UpdateProperties(existingEntity, updatedEntitiesDict[existingEntity.Id]!);

            // Save changes in DB
            await SaveChangesAsync();
            return existingEntities;
        }

        public virtual async Task<T?> DeleteAsync(TKey key)
        {
            // Get existing entity by key
            var existing = await GetAsync(key);

            // Remove entity if it exists
            return await RemoveEntityAsync(existing);
        }

        public virtual async Task<List<T>> DeleteRangeAsync(IEnumerable<TKey> keys)
        {
            // Get existing entities from DB
            var existingEntities = await GetRangeAsync(keys);

            // Remove range of entities
            return await RemoveEntityRangeAsync(existingEntities);
        }


        protected virtual async Task<bool> KeyExistsInHierarchyAsync(TKey key)
        {
            // Check if the hierarchy key exists in the database
            return await _hDbSet.AnyAsync(e => e.Id.Equals(key));
        }

        protected virtual async Task<List<TKey>> KeyRangeExistsInHierarchyAsync(IEnumerable<TKey> keys)
        {
            // Check if any of the hierarchy keys exist in the database
            return await _hDbSet
                .Where(e => keys.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync();
        }
        protected virtual async Task<T?> GetEntityAsync(T entity)
        {
            // Get entity by id
            return await _dbSet.FindAsync(entity.Id);
        }

        protected virtual async Task<List<T>> GetEntityRangeAsync(IEnumerable<T> entities)
        {
            // Get range of entities
            return await GetRangeAsync(entities.Select(e => e.Id));
        }

        protected virtual async Task<T?> RemoveEntityAsync(T? entity)
        {
            // If entity is null, return null
            if (entity is null) return null;

            // Remove entity from DB
            _dbSet.Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        protected virtual async Task<List<T>> RemoveEntityRangeAsync(IEnumerable<T> entities)
        {
            // If no entities to remove, return empty list
            if (!entities.Any()) return [];

            // Remove range of entities
            _dbSet.RemoveRange(entities);
            await SaveChangesAsync();
            return entities.ToList(); // Return removed entities
        }

        protected virtual void UpdateProperties(T existingEntity, T updatedEntity)
        {
            // Update properties of existing entity with values from updated entity
            _dbSet.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        }

        protected virtual async Task SaveChangesAsync()
        {
            // Save changes in DB
            await _context.SaveChangesAsync();
        }
    }
    public abstract class RepositoryBase<T, TKey>(
        ApplicationDbContext context
        ) : RepositoryBase<T, T, TKey>(context)
        where T : class, IEntity<TKey>
        where TKey : notnull
    {

    }
}