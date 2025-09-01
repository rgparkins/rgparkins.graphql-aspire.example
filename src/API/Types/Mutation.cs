using API.Services;
using HotChocolate.Subscriptions;

namespace API.Types;

public class Mutation
{
    public async Task<CreateBookPayload> CreateBookAsync(
        CreateBookInput input,
        [Service] BookService svc)
    {
        if (string.IsNullOrWhiteSpace(input.Title))
            throw new GraphQLException(ErrorBuilder.New()
                .SetMessage("Title is required.")
                .SetCode("VALIDATION_ERROR")
                .Build());

        var book = svc.AddBook(input.Title.Trim(), input.PublishedAt, input.AuthorId);
        return new CreateBookPayload(book.Id, book.Title);
    }

    public async Task<CreateReviewPayload> CreateReviewAsync(
        CreateReviewInput input,
        [Service] BookService svc,
        [Service] ITopicEventSender sender)
    {
        if (input.Rating is < 1 or > 5)
            throw new GraphQLException(ErrorBuilder.New()
                .SetMessage("Rating must be between 1 and 5.")
                .SetCode("VALIDATION_ERROR").Build());

        var review = svc.AddReview(input.BookId, input.Reviewer, input.Rating, input.Comment);
        await sender.SendAsync(nameof(Subscription.OnReviewAdded), review);
        return new CreateReviewPayload(review.Id, review.Rating);
    }
}