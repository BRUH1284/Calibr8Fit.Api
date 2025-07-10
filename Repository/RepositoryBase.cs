using Calibr8Fit.Api.Data;

public abstract class RepositoryBase
{
    protected readonly ApplicationDbContext _context;
    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }
}