using System.Linq.Expressions;

namespace CrossCutting.Domain.SeedWork;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellation = default);
    IRepository<T> GetRepository<T>() where T : class, IAggregateRoot;
    int CountLastInsert<T>() where T : class, IAggregateRoot;
}