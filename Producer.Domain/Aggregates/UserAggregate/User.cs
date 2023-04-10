using Producer.Domain.SeedWork;

namespace Producer.Domain.Aggregates.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
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
            PhoneNumber = phoneNumber;
            Email = email;
            Name = name;
            LastName = lastName;
            Patronymic = patronymic;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}