using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Model;
using NetTopologySuite.Geometries;
using System.Reflection;
using Domain;
using ZM.Domain;

namespace Shipping.Infrastructure.Data;

public partial class AppDbContext : DbContext, IAppDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly SoftDeleteEntitySaveChangesInterceptor _softDeleteEntitySaveChangesInterceptor;
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
                AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        SoftDeleteEntitySaveChangesInterceptor softDeleteEntitySaveChangesInterceptor
        ) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _softDeleteEntitySaveChangesInterceptor = softDeleteEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ship>().HasQueryFilter(b => !b.Deleted);
        modelBuilder.Entity<Port>().HasQueryFilter(b => !b.Deleted);

        base.OnModelCreating(modelBuilder);

        SeedTemplate(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (_softDeleteEntitySaveChangesInterceptor != null)
        {
            optionsBuilder.AddInterceptors(_softDeleteEntitySaveChangesInterceptor);
        }
        if (_auditableEntitySaveChangesInterceptor != null)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    private void SeedTemplate(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ship>().HasData(
            new Ship
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(68.2, 88) { SRID = 4326 },
                Name = "Ship 1",
                Id = Guid.NewGuid(),
                Velocity = 30
            },
            new Ship
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(4.2, 78.2) { SRID = 4326 },
                Name = "Ship 2",
                Id = Guid.NewGuid(),
                Velocity = 40
            },
            new Ship
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(11.2, 23.2) { SRID = 4326 },
                Name = "Ship 3",
                Id = Guid.NewGuid(),
                Velocity = 32
            },
            new Ship
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(27.22, 13.2) { SRID = 4326 },
                Name = "Ship 4",
                Id = Guid.NewGuid(),
                Velocity = 45
            });

        modelBuilder.Entity<Port>().HasData(
            new Port
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(11, 23.7) { SRID = 4326 },
                Name = "Port 1",
                Id = Guid.NewGuid()
            },
            new Port
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(76.2, 75.2) { SRID = 4326 },
                Name = "Port 2",
                Id = Guid.NewGuid()
            },
            new Port
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(32.23, 33.4) { SRID = 4326 },
                Name = "Port 3",
                Id = Guid.NewGuid()
            },
            new Port
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(33.23, 35.2) { SRID = 4326 },
                Name = "Port 4",
                Id = Guid.NewGuid()
            },
            new Port
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(1.97, 32.2) { SRID = 4326 },
                Name = "Port 5",
                Id = Guid.NewGuid()
            },
            new Port
            {
                CreatedDate = DateTime.UtcNow,
                Geolocation = new Point(9.34, 36.34) { SRID = 4326 },
                Name = "Port 6",
                Id = Guid.NewGuid()
            });

    }
}
