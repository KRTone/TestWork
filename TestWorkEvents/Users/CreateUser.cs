namespace TestWorkEvents.Users
{
    public class CreateUser
    {
        public CreateUser()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; }
        public string PhoneNumber { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? Patronymic { get; init; }
    }
}