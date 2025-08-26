using System.Linq.Expressions;

namespace N_LayerBestPratice.Repository.Abstract;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(bool trackChanges = false);
    
    IQueryable<T> Where(Expression<Func<T, bool>> predicate , bool trackChanges = false);
    
    Task<T?> GetByIdAsync(int id);
    
    Task AddAsync(T entity);
    
    void Update(T entity);
    
    void Delete(T entity);
    
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    
}