using System.Linq.Expressions;

namespace CrossCutting.Domain.SeedWork;

public interface IRepository
{
    Task<bool> Exist(int id, CancellationToken cancellation);
    Task<bool> Any(CancellationToken cancellation = default);
    Task<long> Count(CancellationToken cancellation = default);
}

public interface IRepository<T> : IRepository where T : class, IAggregateRoot
{
    /// <summary>
    /// Adicionar uma entidade ao contexto
    /// </summary>
    /// <param name="entity">Entidade que herda de IAggregateRoot</param>
    /// <param name="cancellation"></param>
    /// <remarks>Utilizar após todos os Add, o método <see cref="IUnitOfWork.SaveChanges"/> do unit of work</remarks>
    Task Add(T entity, CancellationToken cancellation = default);
    /// <summary>
    /// Adicionar uma entidade ao contexto
    /// </summary>
    /// <param name="entity">Entidade que herda de IAggregateRoot</param>
    /// <param name="cancellation"></param>
    /// <remarks>Utilizar após o AddRange, o método <see cref="IUnitOfWork.SaveChanges"/> do unit of work</remarks>
    Task AddRange(IEnumerable<T> entities, CancellationToken cancellation = default);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellation"></param>
    /// <remarks>Este faz o insert de forma atômica, não se utiliza do <see cref="IUnitOfWork.SaveChanges"/></remarks>
    Task BulkAddRange(IEnumerable<T> entities, bool ignoreIfExist = true, CancellationToken cancellation = default);
    Task BulkAdd(T entity, bool ignoreIfExist = true, CancellationToken cancellation = default);
    Task Update(T entity, CancellationToken cancellation = default);
    Task BulkUpdate(T entity, CancellationToken cancellation = default);
    Task Delete(T entity, CancellationToken cancellation = default);
    Task BulkDelete(T entity, CancellationToken cancellation = default);
    Task<T?> Find(T id, CancellationToken cancellation = default);
    Task<T?> Find(int id, CancellationToken cancellation = default);
    Task<T?> Find(Guid id, CancellationToken cancellation = default);
    Task<IEnumerable<T>> Query(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
    Task<IEnumerable<T>> QueryAll(CancellationToken cancellation = default);
    Task<bool> Any(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
    Task<long> Count(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty">Máximo valor para a propriedade selecionada</typeparam>
    /// <param name="selector">Expressão para selecionar a propriedade</param>
    /// <returns></returns>
    Task<TProperty?> Max<TProperty>(Expression<Func<T, TProperty>> selector, TProperty defaultValue = default, CancellationToken cancellation = default);
}