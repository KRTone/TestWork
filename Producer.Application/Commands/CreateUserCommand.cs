using MediatR;

namespace Producer.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? Patronymic { get; init; }
        public string PhoneNumber { get; init; } = null!;
        public string Email { get; init; } = null!;
    }
}