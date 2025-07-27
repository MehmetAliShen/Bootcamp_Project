using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repositories.Data;

namespace Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly BootcampContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BootcampContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();
        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public DbContext Context => _context;

        public TEntity GetById(int id) => _dbSet.Find(id);
        public IEnumerable<TEntity> GetAll() => _dbSet.ToList();
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).ToList();
        public void Add(TEntity entity) => _dbSet.Add(entity);
        public void Update(TEntity entity) => _dbSet.Update(entity);
        public void Delete(TEntity entity) => _dbSet.Remove(entity);
        public void SaveChanges() => _context.SaveChanges();
    }
}
