using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

public abstract class RepositoryBase<T>(
    ApplicationDbContext context
    ) : IRepositoryBase<T>
    where T : class, IEntity
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T?> GetAsync(params object[] key)
    {
        // Get entity by key
        return await _dbSet.FindAsync(key);
    }
    public virtual async Task<List<T>> GetRangeAsync(IEnumerable<object[]> keys)
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

    public virtual async Task<bool> ExistsAsync(object[] key)
    {
        // Check if entity exists by key
        return await GetAsync(key) is not null;
    }
    public virtual async Task<T?> AddAsync(T entity)
    {
        // Check if entity already exists
        if (await GetEntityAsync(entity) is not null) return null;

        // Add new entity to DB
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<List<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        var existingEntities = await GetEntityRangeAsync(entities);

        // Filter out existing entities
        var newEntities = entities
            .Where(e => !existingEntities.Any(existing => existing.Id.Equals(e.Id)))
            .ToList();

        // If no new entities to add, return empty list
        if (newEntities.Count == 0) return [];

        // Add range of entities to DB
        await _dbSet.AddRangeAsync(newEntities);
        await SaveChangesAsync();
        return newEntities;
    }
    public virtual async Task<T> AddOrUpdateAsync(T entity)
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

    public virtual async Task<T?> DeleteAsync(object[] key)
    {
        // Get existing entity by key
        var existing = await GetAsync(key);

        // Remove entity if it exists
        return await RemoveEntityAsync(existing);
    }

    public virtual async Task<List<T>> DeleteRangeAsync(IEnumerable<object[]> keys)
    {
        // Get existing entities from DB
        var existingEntities = await GetRangeAsync(keys);

        // Remove range of entities
        return await RemoveEntityRangeAsync(existingEntities);
    }


    protected virtual async Task<T?> GetEntityAsync(T entity)
    {
        // Get entity by id
        return await _dbSet.FindAsync(entity.Id);
    }
    protected virtual async Task<List<T>> GetEntityRangeAsync(IEnumerable<T> entities)
    {
        // Get range of entities
        return await _dbSet
            .Where(e => entities.Select(entity => entity.Id).Contains(e.Id))
            .ToListAsync();
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