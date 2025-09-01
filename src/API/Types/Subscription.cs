using API.Models;

namespace API.Types;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Review OnReviewAdded([EventMessage] Review review) => review;
}