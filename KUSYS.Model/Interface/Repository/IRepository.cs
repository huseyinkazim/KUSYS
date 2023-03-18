using KUSYS.Data.Entity.Base;
using System.Linq.Expressions;


namespace KUSYS.Data.Interface.Repository
{
    public interface IRepository<TEntity,TType> where TEntity : EntityBase<TType>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(TType id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
    }
}
