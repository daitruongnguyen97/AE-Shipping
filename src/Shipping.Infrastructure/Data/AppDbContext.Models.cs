using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Model;

namespace Shipping.Infrastructure.Data
{
    public partial class AppDbContext
    {
        public DbSet<Ship> Ship { get; set; }
        public DbSet<Port> Port { get; set; }
    }
}
