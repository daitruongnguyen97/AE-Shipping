using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain;
using Shipping.Infrastructure.Data;

namespace Shipping.Infrastructure;

public class LinqRepository<T> : LinqRepositoryBase<T>, ILinqRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public override DbContext Context
    {
        get
        {
            return _context;
        }
    }

    public LinqRepository(AppDbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
