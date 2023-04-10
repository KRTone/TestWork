using Consumer.Domain.Events;
using Consumer.Domain.Exceptions;
using Consumer.Domain.SeedWork;
using System.Text;

namespace Consumer.Domain.Aggregates.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        private Guid? _organizationGuid;

        public string PhoneNumber { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string? Patronymic { get; private set; }

        protected User()
        {
        }

        public User(Guid guid, string phoneNumber, string email, string name,
            string lastName, string? patronymic) : this()
        {
            Guid = guid;
            PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? throw new ConsumerDomainException(nameof(phoneNumber)) : phoneNumber;
            Email = string.IsNullOrWhiteSpace(email) ? throw new ConsumerDomainException(nameof(email)) : email;
            Name = string.IsNullOrWhiteSpace(name) ? throw new ConsumerDomainException(nameof(name)) : name;
            LastName = string.IsNullOrWhiteSpace(lastName) ? throw new ConsumerDomainException(nameof(lastName)) :lastName;
            Patronymic = patronymic;

            AddDomainEvent(new UserCreatedEvent(this));
        }

        public void MoveToOrganization(Guid organizationGuid)
        {
            if (organizationGuid != _organizationGuid)
            {
                AddDomainEvent(new UserMovedToOrganizationEvent(_organizationGuid, Guid));
                _organizationGuid = organizationGuid;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append($"{nameof(Guid)}:{Guid};");
            builder.Append($"{nameof(PhoneNumber)}:{PhoneNumber};");
            builder.Append($"{nameof(Email)}:{Email};");
            builder.Append($"{nameof(Name)}:{Name};");
            builder.Append($"{nameof(LastName)}:{LastName};");
            builder.Append($"{nameof(Patronymic)}:{Patronymic};");
            builder.Append($"OrganizationGuid:{_organizationGuid}");
            return builder.ToString();
        }
    }
}