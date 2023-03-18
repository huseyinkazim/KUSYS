using KUSYS.Data.Entity.Base;
using KUSYS.Data.Interface.Repository;
using KUSYS.Repository.DataBase;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Repository
{
    public class Repository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : EntityBase<TType>
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbContext.Set<TEntity>().Update(entity);           
        }
        public void Delete(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.isDeleted = true;
            _dbContext.Set<TEntity>().Update(entity);//Soft Delete
            //_dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().Where(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public TEntity GetById(TType id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }


    }
}