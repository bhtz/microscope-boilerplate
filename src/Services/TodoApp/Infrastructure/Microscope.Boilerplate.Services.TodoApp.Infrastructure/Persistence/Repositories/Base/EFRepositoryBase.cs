using System.Linq.Expressions;
using Microscope.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories.Base;

public class EFRepositoryBase<TEntity> : EFRepositoryBase<TEntity, Guid>, IBaseRepository<TEntity> where TEntity : class, IAggregateRoot<Guid>
{
    public EFRepositoryBase(TodoAppDbContext context) : base(context)
    {
    }
}

public class EFRepositoryBase<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class, IAggregateRoot<TPrimaryKey>
{
    protected readonly TodoAppDbContext _context;

    public EFRepositoryBase(TodoAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var added = await DbSet.AddAsync(entity);
        return added.Entity;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        AttachIfNot(entity);
        DbSet.Remove(entity);

        return Task.FromResult(true);
    }

    public virtual Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate = null, string includes = null)
    {
        IQueryable<TEntity> query = DbSet;
        
        if (!string.IsNullOrEmpty(includes))
            query = query.Include(includes);

        if(predicate is not null)
            query = query.Where(predicate);
        
        return Task.FromResult(query.AsEnumerable());
    }

    public virtual Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null, string includes = null)
    {
        IQueryable<TEntity> query = DbSet;
        
        if (!string.IsNullOrEmpty(includes))
            query = query.Include(includes);
        
        return query.SingleOrDefaultAsync(predicate);
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.AsQueryable().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(TPrimaryKey id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        AttachIfNot(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(entity);
    }

    private void AttachIfNot(TEntity entity)
    {
        var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
        if (entry != null)
        {
            return;
        }

        DbSet.Attach(entity);
    }

    public IQueryable<TEntity> Query()
    {
        return DbSet.AsQueryable();
    }
}