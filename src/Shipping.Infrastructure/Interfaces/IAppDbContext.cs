using Microsoft.EntityFrameworkCore;
using Domain;
using Shipping.Domain.Model;

namespace Shipping.Infrastructure
{
    public interface IAppDbContext
    {
        int SaveChanges();
        public DbSet<Ship> Ship { get; set; }
        public DbSet<Port> Port { get; set; }

    }
}
