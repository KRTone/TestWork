using MediatR;

namespace Consumer.Application.Commands
{
    public class MoveUserToOrganizationCommand : IRequest
    {
        public Guid OrganizationGuid { get; init; }
        public Guid UserGuid { get; init; }
    }
}