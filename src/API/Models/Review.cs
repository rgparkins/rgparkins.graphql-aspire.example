namespace API.Models;

public class Review
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid BookId { get; set; }
    public string Reviewer { get; set; } = default!;
    public int Rating { get; set; } // 1..5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}