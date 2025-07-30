using Calibr8Fit.Api.Data;

public abstract class RepositoryBase(ApplicationDbContext context)
{
    protected readonly ApplicationDbContext _context = context;
}