using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.Exceptions;
using Consumer.Domain.SeedWork;
using System.Text;

namespace Consumer.Domain.Aggregates.OrganizationAggregate
{
    public class Organization : Entity, IAggregateRoot
    {
        public string Name { get; private set; } = null!;


        private List<User> _users = new List<User>();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        protected Organization()
        {
        }

        public Organization(Guid guid, string name)
        {
            Guid = guid;
            Name = string.IsNullOrWhiteSpace(name) ? throw new ConsumerDomainException(nameof(name)) : name;
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append($"{nameof(Guid)}:{Guid};");
            builder.Append($"{nameof(Name)}:{Name}");
            return builder.ToString();
        }

        //public User AddUser(Guid userGuid, string phoneNumber, string email, string name, string lastName, string? patronymic)
        //{
        //    User? existingUser = _users.SingleOrDefault(x => x.Guid == userGuid);

        //    if (existingUser != null)
        //    {
        //        AddDomainEvent(new UserAddedToOrganizationEvent(Guid, userGuid));

        //        return existingUser;
        //    }

        //    User user = new User(phoneNumber, email, name, lastName, patronymic);

        //    _users.Add(user);

        //    AddDomainEvent(new UserAddedToOrganizationEvent(Guid, userGuid));

        //    return user;
        //}
    }
}