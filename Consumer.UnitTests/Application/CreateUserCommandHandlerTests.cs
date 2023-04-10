using Consumer.Application.Commands;
using Microsoft.Extensions.Logging;
using Moq;

namespace Consumer.UnitTests.Application
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public CreateUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(buyerRepo => buyerRepo.UnitOfWork.SaveEntitiesAsync(default)).Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task Handle_return_guid()
        {
            var loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();

            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "name";
            string lastName = "lasnName";
            string patronymic = "";
            CreateUserCommand command = new CreateUserCommand
            {
                Guid = guid,
                Email = email,
                LastName = lastName,
                Name = name,
                Patronymic = patronymic,
                PhoneNumber = phone
            };

            CreateUserCommandHandler handler = new CreateUserCommandHandler(_userRepositoryMock.Object, loggerMock.Object);
            var token = new CancellationToken();
            var result = await handler.Handle(command, token);

            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
