using System.Linq.Expressions;
using ACME.Backend.Entities;

namespace ACME.Backend.Interfaces;
public interface IRepository<T> where T: Entity
{
    Task<ICollection<T>> GetPagedAsync(int start = 0, int count = 10);
    Task<T?> GetAsync(uint id);
    Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate, int start = 0, int count = 10);
    Task<T> InsertAsync(T item);
    Task<bool> UpdateAsync(uint id, T item);
    Task<bool> DeleteAsync(uint id);
    Task<int> SaveChangesAsync();
}
