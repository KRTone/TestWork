using Consumer.Application.Commands;
using Consumer.Domain.SeedWork;
using Microsoft.Extensions.Logging;
using Moq;

namespace Consumer.UnitTests.Application
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _context;

        public CreateUserCommandHandlerTests()
        {
            _context = new Mock<IUnitOfWork>();
            _context.Setup(buyerRepo => buyerRepo.SaveEntitiesAsync(default)).Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task Handle_return_guid()
        {
            var loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
            _context
                .Setup(x => x.UserRepository.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<User>());

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

            CreateUserCommandHandler handler = new CreateUserCommandHandler(_context.Object, loggerMock.Object);
            var token = new CancellationToken();
            var result = await handler.Handle(command, token);

            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
