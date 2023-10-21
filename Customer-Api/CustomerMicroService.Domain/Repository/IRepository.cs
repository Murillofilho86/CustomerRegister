using System.Linq.Expressions;

namespace CustomerMicroService.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity model);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

        void Update(TEntity model);

        void Remove(TEntity model);

        void Dispose();
    }
}
