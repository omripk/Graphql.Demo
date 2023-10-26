using GraphQL.Demo.Api.Schema.Mutations;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQL.Demo.Api.Schema.Subscriptions;

public class CourseSubscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

    [SubscribeAndResolve]
    public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
    {
        string topicName = $"{courseId}_{nameof(CourseSubscription.CourseUpdated)}";

        return  topicEventReceiver.SubscribeAsync<CourseResult>(topicName, CancellationToken.None);
    }
}