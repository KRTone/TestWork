namespace Consumer.WebApi.ViewModels
{
    public record User(Guid guid, string phoneNumber, string email, string name, string lastName, string? patronymic = default);
}
