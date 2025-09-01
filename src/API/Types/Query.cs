using API.Models;
using API.Services;
using API.DataLoaders;
using HotChocolate.Data;

namespace API.Types;

public class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks([Service] BookService svc) => svc.QueryBooks();

    [UseFiltering, UseSorting]
    public IQueryable<Author> GetAuthors([Service] BookService svc) => svc.QueryAuthors();

    [UseFiltering, UseSorting]
    public IQueryable<Review> GetReviews([Service] BookService svc) => svc.QueryReviews();

    public async Task<Author?> GetAuthorByIdAsync(Guid id, AuthorByIdDataLoader loader, CancellationToken ct)
        => await loader.LoadAsync(id, ct);
}