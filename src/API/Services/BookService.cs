using API.Data;
using API.Models;

namespace API.Services;

public class BookService
{
    private readonly InMemoryStore _db;
    public BookService(InMemoryStore db) => _db = db;

    public IQueryable<Book> QueryBooks() => _db.Books.AsQueryable();
    public IQueryable<Author> QueryAuthors() => _db.Authors.AsQueryable();
    public IQueryable<Review> QueryReviews() => _db.Reviews.AsQueryable();

    public Book AddBook(string title, DateTime publishedAt, Guid authorId)
    {
        var book = new Book { Title = title, PublishedAt = publishedAt, AuthorId = authorId };
        _db.Books.Add(book);
        return book;
    }

    public Review AddReview(Guid bookId, string reviewer, int rating, string? comment)
    {
        var r = new Review { BookId = bookId, Reviewer = reviewer, Rating = rating, Comment = comment };
        _db.Reviews.Add(r);
        return r;
    }
}