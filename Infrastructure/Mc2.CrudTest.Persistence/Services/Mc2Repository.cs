using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Common.Pagination;
using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Persistence.Services
{
    public sealed class Mc2Repository<T> : IMc2Repository<T> where T : BaseEntity
    {
        #region Fields

        private readonly DbSet<T> _dbSet;
        private readonly IMc2DbContext _dbContext;

        #endregion Fields

        #region Ctor
        public Mc2Repository(IMc2DbContext dbContext)
        {
            this._dbContext = dbContext ??
                 throw new ArgumentNullException(paramName: nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        #endregion Ctor
        public DbSet<T> Get => _dbSet;

        public IQueryable<T> Table => _dbSet.AsQueryable();

        public IReadOnlyList<T> GetAll(bool trackChanges = true)
        {
            return trackChanges ? _dbSet.ToList() : _dbSet.AsNoTracking().ToList();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(bool trackChanges)
        {
            return trackChanges ? await _dbSet.ToListAsync() : await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            if (id is null or 0)
                return null;
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id.Value);      
        }

        public async Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            IReadOnlyList<T> list = await GetAllAsync(true);
            return list.ToPaged(page: pageNumber, pageSize).ToList();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            try
            {
                _dbSet.Update(entity);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await _dbContext.SaveChangesAsync();
            }

        }
        public async Task DeleteAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}