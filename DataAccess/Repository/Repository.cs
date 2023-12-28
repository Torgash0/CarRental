using System.Linq.Expressions;

using DataAccess.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repository;

public class Repository<T>(IDbContextFactory<CarRentalDbContext> contextFactory) : IRepository<T> where T : class, IBaseEntity
{
    private readonly IDbContextFactory<CarRentalDbContext> _contextFactory = contextFactory;

    public IEnumerable<T> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();

        return context.Set<T>().ToList();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
    {
        using var context = _contextFactory.CreateDbContext();

        return context.Set<T>().Where(filter).ToList();
    }

    public T? GetById(int id)
    {
        using var context = _contextFactory.CreateDbContext();

        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public T? GetById(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();

        return context.Set<T>().FirstOrDefault(x => x.ExternalId == id);
    }

    public T Save(T entity)
    {
        using var context = _contextFactory.CreateDbContext();

        EntityEntry<T>? result = null;

        if (context.Set<T>().Any(x => x.Id == entity.Id))
        {
            entity.ModificationTime = DateTime.UtcNow;
            result = context.Set<T>().Attach(entity);

            context.Entry(entity).State = EntityState.Modified;
        }
        else
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = entity.CreationTime;
            result = context.Set<T>().Add(entity);
        }

        context.SaveChanges();

        return result.Entity;
    }

    public void Delete(T entity)
    {
        using var context = _contextFactory.CreateDbContext();

        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Deleted;

        context.SaveChanges();
    }
}
