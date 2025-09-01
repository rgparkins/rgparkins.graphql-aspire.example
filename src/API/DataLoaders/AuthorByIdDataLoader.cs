using API.Data;
using API.Models;

namespace API.DataLoaders;

public class AuthorByIdDataLoader : BatchDataLoader<Guid, Author>
{
    private readonly InMemoryStore _db;

    public AuthorByIdDataLoader(IBatchScheduler scheduler, InMemoryStore db)
        : base(scheduler, new DataLoaderOptions()) => _db = db;

    protected override Task<IReadOnlyDictionary<Guid, Author>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var dict = _db.Authors.Where(a => keys.Contains(a.Id)).ToDictionary(a => a.Id);
        return Task.FromResult((IReadOnlyDictionary<Guid, Author>)dict);
    }
}