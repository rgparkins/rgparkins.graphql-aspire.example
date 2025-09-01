namespace API.Types;

public record CreateBookInput(string Title, DateTime PublishedAt, Guid AuthorId);
public record CreateBookPayload(Guid Id, string Title);

public record CreateReviewInput(Guid BookId, string Reviewer, int Rating, string? Comment);
public record CreateReviewPayload(Guid Id, int Rating);