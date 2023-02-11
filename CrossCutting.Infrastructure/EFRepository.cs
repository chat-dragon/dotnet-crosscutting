using CrossCutting.Domain.SeedWork;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CrossCutting.Infrastructure;

public class EFRepository<TAggregateRoot, TContext> : IRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
    where TContext : DbContext
{
    private TContext _context;

    public EFRepository(TContext context) => _context = context;

    protected TContext Context { get => _context; set => _context = value; }

    public async Task Add(TAggregateRoot entity, CancellationToken cancellation = default)
    {
        _ = await Context.AddAsync(entity, cancellation);
    }

    public async Task AddRange(IEnumerable<TAggregateRoot> entities, CancellationToken cancellation = default)
    {
        await Context.AddRangeAsync(entities, cancellation);
    }

    public Task<bool> Any(CancellationToken cancellation = default)
    {
        return _context.Set<TAggregateRoot>().AnyAsync(cancellation);
    }

    public Task<bool> Any(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellation = default)
    {
        return _context.Set<TAggregateRoot>().AnyAsync(predicate, cancellation);
    }

    public Task BulkAdd(TAggregateRoot entity, bool ignoreIfExist = true, CancellationToken cancellation = default)
    {
        var list = new List<TAggregateRoot> { entity };
        return BulkAddRange(list, ignoreIfExist, cancellation);
    }

    public Task BulkAddRange(IEnumerable<TAggregateRoot> entities, bool ignoreIfExist = true, CancellationToken cancellation = default)
    {
        var config = new BulkConfig()
        {
            //BatchSize = 100,
            BulkCopyTimeout = 0,
            SetOutputIdentity = true,
            PropertiesToIncludeOnUpdate = new List<string> { "" }
        };
        if(ignoreIfExist)
        {
            return _context.BulkInsertOrUpdateAsync(entities.ToList(), bulkConfig: config, cancellationToken: cancellation);
        }
        return _context.BulkInsertAsync(entities.ToList(), bulkConfig: config, cancellationToken: cancellation);
    }

    public Task Delete(TAggregateRoot entity, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exist(int id, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
    public Task<TAggregateRoot?> Find(TAggregateRoot id, CancellationToken cancellation = default)
    {
        if (id is IAggregateIntRoot intId)
        {
            return Find(intId.Id, cancellation);
        }
        else if (id is IAggregateGuidRoot guidId)
        {
            return Find(guidId.Uuid, cancellation);
        }
        throw new NotImplementedException();
    }
    public async Task<TAggregateRoot?> Find(int id, CancellationToken cancellation = default)
    {
        return await _context.FindAsync<TAggregateRoot>(id, cancellation);
    }
    public async Task<TAggregateRoot?> Find(Guid id, CancellationToken cancellation = default)
    {
        return await _context.FindAsync<TAggregateRoot>(id, cancellation);
    }

    public async Task<TProperty?> Max<TProperty>(Expression<Func<TAggregateRoot, TProperty>> selector, TProperty defaultValue = default, CancellationToken cancellation = default)
    {
        if (!await _context.Set<TAggregateRoot>().AnyAsync())
        {
            return defaultValue;
        }

        return await _context.Set<TAggregateRoot>().MaxAsync(selector, cancellation);
    }

    public Task Update(TAggregateRoot entity, CancellationToken cancellation = default)
    {
        var entry = Context.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<long> Count(CancellationToken cancellation = default)
    {
        return await _context.Set<TAggregateRoot>().LongCountAsync(cancellation);
    }

    public async Task<long> Count(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellation = default)
    {
        return await _context.Set<TAggregateRoot>().LongCountAsync(predicate, cancellation);
    }

    public Task BulkDelete(TAggregateRoot entity, CancellationToken cancellation = default)
    {
        var list = new List<TAggregateRoot> { entity };
        var config = new BulkConfig() { SetOutputIdentity = true };
        return _context.BulkDeleteAsync(list, bulkConfig: config, cancellationToken: cancellation);
    }

    public Task BulkUpdate(TAggregateRoot entity, CancellationToken cancellation = default)
    {
        var list = new List<TAggregateRoot> { entity };
        var config = new BulkConfig();
        return _context.BulkUpdateAsync(list, bulkConfig: config, cancellationToken: cancellation);
    }

    public async Task<IEnumerable<TAggregateRoot>> Query(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellation = default)
    {
        return await _context.Set<TAggregateRoot>()
            .Where(predicate)
            .ToListAsync(cancellation);
    }

    public async Task<IEnumerable<TAggregateRoot>> QueryAll(CancellationToken cancellation = default)
    {
        return await _context.Set<TAggregateRoot>()
            .ToListAsync(cancellation);
    }
}
