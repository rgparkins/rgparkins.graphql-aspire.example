using API.DataLoaders;
using API.Models;
using HotChocolate;

namespace API.Types;

[ObjectType<Book>]
public class BookResolvers
{
    public async Task<Author> AuthorAsync(
        [Parent] Book book,
        AuthorByIdDataLoader loader,
        CancellationToken ct)
        => await loader.LoadAsync(book.AuthorId, ct);
}