namespace API.Models;

public class Book
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public DateTime PublishedAt { get; set; }
    public Guid AuthorId { get; set; }
}