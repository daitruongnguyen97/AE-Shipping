using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Domain;

public interface ILinqRepository<T>
{
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                   List<Expression<Func<T, object>>> includes = null,
                                   bool disableTracking = true,
                                   bool ignoreQueryFilter = false);
    IQueryable<T> List(Expression<Func<T, bool>> predicate = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<T> GetByIdAsync(object id);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken);
    Task SaveChangeAsync(CancellationToken cancellationToken);
    Task ExecuteSqlCommand(string command);
}


public class LinqRepositoryBase<T> where T : class
{
    protected readonly DbContext _dbContext;

    public virtual DbContext Context
    {
        get
        {
            return _dbContext;
        }
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true, bool ignoreQueryFilter = false)
    {
        IQueryable<T> query = Context.Set<T>();
        if (ignoreQueryFilter) query = query.IgnoreQueryFilters();
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual IQueryable<T> List(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = Context.Set<T>();

        if (predicate != null) query = query.Where(predicate);

        return query;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().Where(predicate).AnyAsync();
    }

    public virtual async Task<T> GetByIdAsync(object id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        Context.Set<T>().AddRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        Context.Set<T>().UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        Context.Set<T>().RemoveRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task ExecuteSqlCommand(string command)
    {
        await Context.Database.ExecuteSqlRawAsync(command);
    }
}
