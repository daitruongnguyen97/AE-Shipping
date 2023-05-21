using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Shipping.Infrastructure.Data;

public class AppDbContextInitialiser
{
    private readonly ILogger<AppDbContextInitialiser> _logger;
    private readonly AppDbContext _context;

    public AppDbContextInitialiser(
        ILogger<AppDbContextInitialiser> logger,
        AppDbContext context
        )
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (!_context.Database.CanConnect() || _context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}
