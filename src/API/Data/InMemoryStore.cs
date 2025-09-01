using API.Models;

namespace API.Data;

public class InMemoryStore
{
    public List<Author> Authors { get; } = new();
    public List<Book> Books { get; } = new();
    public List<Review> Reviews { get; } = new();
}