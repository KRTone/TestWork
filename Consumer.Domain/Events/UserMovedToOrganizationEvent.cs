using MediatR;

namespace Consumer.Domain.Events
{
    public record UserMovedToOrganizationEvent(Guid? OrganizationGuid, Guid UserGuid) : INotification;
}