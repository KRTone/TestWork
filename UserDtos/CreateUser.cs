namespace UserDtos
{
    public class CreateUser
    {
        public Guid Guid { get; init; }
        public string PhoneNumber { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? Patronymic { get; init; }
    }
}