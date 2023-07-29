using System.Linq.Expressions;

namespace Microscope.SharedKernel;

public interface IRepository
{
    // IUnitOfWork UnitOfWork { get; }
}

public interface IRepository<T> : IRepository<T, Guid> where T : class, IAggregateRoot<Guid>
{

}

public interface IRepository<T, TId> : IRepository where T : class, IAggregateRoot<TId>
{

}

public interface IBaseRepository<T, TId> : IRepository where T : class, IAggregateRoot<TId>
{
    IQueryable<T> Query();

    Task<T> GetByIdAsync(TId id);

    Task<List<T>> GetAllAsync();  

    Task<T> Find(Expression<Func<T, bool>> predicate = null, string includes = null);

    Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate = null, string includes = null);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}

public interface IBaseRepository<T> : IBaseRepository<T, Guid> where T : class, IAggregateRoot<Guid>
{

}