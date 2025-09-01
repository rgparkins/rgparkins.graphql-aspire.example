using API.Models;

namespace API.Data;

public static class Seed
{
    public static void EnsureSeeded(InMemoryStore db)
    {
        if (db.Authors.Any()) return;

        var a1 = new Author { Name = "Ada Lovelace", Email = "ada@example.com" };
        var a2 = new Author { Name = "Alan Turing",  Email = "alan@example.com" };

        db.Authors.AddRange([a1, a2]);

        var b1 = new Book { Title = "Analytical Engines", PublishedAt = DateTime.UtcNow.AddYears(-5), AuthorId = a1.Id };
        var b2 = new Book { Title = "On Computable Numbers", PublishedAt = DateTime.UtcNow.AddYears(-8), AuthorId = a2.Id };
        var b3 = new Book { Title = "Enigmas & Codes", PublishedAt = DateTime.UtcNow.AddYears(-2), AuthorId = a2.Id };

        db.Books.AddRange([b1, b2, b3]);

        db.Reviews.AddRange(new[]
        {
            new Review { BookId = b1.Id, Reviewer = "Grace", Rating = 5, Comment = "Timeless." },
            new Review { BookId = b2.Id, Reviewer = "Kurt",  Rating = 4, Comment = "Foundational." }
        });
    }
}