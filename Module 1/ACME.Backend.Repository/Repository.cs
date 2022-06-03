using System.Linq.Expressions;
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ACME.Backend.Entities;

namespace ACME.Backend.Repository;
public class Repository<T> : IRepository<T> where T: Entity
{
    protected ShopContext _context;
    private ILogger _logger;

    public Repository(ShopContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async Task<bool> DeleteAsync(uint id)
    {
        var dbEntity = await _context.FindAsync<T>(id);
        if (dbEntity == null) 
        {
            _logger.LogInformation($"No entry for {id} (Entity: {nameof(T)})");
            return false;
        }
        _context.Remove(dbEntity);
        var nrChanged = await SaveChangesAsync();
        return nrChanged > 0;
    }

    public virtual async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate, int start = 0, int count = 10)
    {
        return await _context.Set<T>()
            .Where(predicate)
            .Skip(start)
            .Take(count)
            .ToListAsync();
    }

    public virtual async Task<T?> GetAsync(uint id)
    {
       return await _context.Set<T>().FirstOrDefaultAsync<T>(item=>item.ID == id);
    }

    public virtual async Task<ICollection<T>> GetPagedAsync(int start = 0, int count = 10)
    {
         return await _context.Set<T>()
            .Skip(start)
            .Take(count)
            .ToListAsync();
    }

    public async Task<T> InsertAsync(T item)
    {
        await _context.Set<T>().AddAsync(item);
        await SaveChangesAsync();
        return item;
    }

    public async Task<int> SaveChangesAsync()
    {
        for(var i = 0; i < 5; i++)
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException exception)
            {
                foreach(var entry in exception.Entries)
                {
                    var current = await entry.GetDatabaseValuesAsync();
                    if (current != null)
                        entry.OriginalValues.SetValues(current);
                }
            }
        }
        return 0;
    }

    public async Task<bool> UpdateAsync(uint id, T item)
    {
        var dbEntity = await _context.FindAsync<T>(id);
        if (dbEntity == null) 
        {
            _logger.LogInformation($"No entry for {id} (Entity: {nameof(T)})");
            return false;
        }
        _context.Entry(dbEntity).CurrentValues.SetValues(item);
        var nrChanged = await SaveChangesAsync();
        return nrChanged > 0;
    }
}
