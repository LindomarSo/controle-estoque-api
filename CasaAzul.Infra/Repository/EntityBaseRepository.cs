using CasaAzul.Domain.Interfaces;
using CasaAzul.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CasaAzul.Infra.Repository
{
    public class EntityBaseRepository<TEntity> : IEntityBaseRepository<TEntity> where TEntity : class
    {
        private readonly EntityContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EntityBaseRepository(EntityContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(TEntity[] entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            return await query.FirstOrDefaultAsync(where);
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            return await query.Where(where).ToListAsync();
        }
    }
}
