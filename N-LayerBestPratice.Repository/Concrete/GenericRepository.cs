using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.DbContext;

namespace N_LayerBestPratice.Repository.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }


    public IQueryable<T> GetAll(bool trackChanges = false)
    {
        // .AsQueryable() opsiyonel yani zaten  where bir IQueryable döndürüyor

        var deneme = _context.Set<T>();

        return trackChanges ? _dbSet.AsQueryable() : _dbSet.AsNoTracking().AsQueryable();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool trackChanges = false) // False ise izlemez
    {
        // .AsQueryable() opsiyonel yani zaten  where bir IQueryable döndürüyor
        return trackChanges
            ? _dbSet.Where(predicate)
            : _dbSet.AsNoTracking().Where(predicate);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity) => _dbSet.Update(entity); // simdilik bu sekilde yapalım sonra daha verimli update metodu yazacağim 


    public void Delete(T entity) => _dbSet.Remove(entity);
}