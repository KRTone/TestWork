using Consumer.Domain.Aggregates.UserAggregate;
using MediatR;

namespace Consumer.Domain.Events
{
    public record UserCreatedEvent(User User) : INotification;
}