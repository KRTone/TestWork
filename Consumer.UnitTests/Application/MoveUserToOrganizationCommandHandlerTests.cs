using Consumer.Application.Commands;
using Consumer.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.UnitTests.Application
{
    public class MoveUserToOrganizationCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _context;

        public MoveUserToOrganizationCommandHandlerTests()
        {
            _context = new Mock<IUnitOfWork>();
            
            _context.Setup(buyerRepo => buyerRepo.SaveEntitiesAsync(default)).Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task Handle_success()
        {
            var loggerMock = new Mock<ILogger<MoveUserToOrganizationCommandHandler>>();

            _context
                .Setup(x => x.UserRepository.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User(It.IsAny<Guid>(), "p", "e", "n", "l", "p"));

            MoveUserToOrganizationCommand command = new MoveUserToOrganizationCommand
            {
                OrganizationGuid = Guid.NewGuid(),
                UserGuid = Guid.NewGuid()
            };

            MoveUserToOrganizationCommandHandler handler = new MoveUserToOrganizationCommandHandler(_context.Object, loggerMock.Object);
            var token = new CancellationToken();
            await handler.Handle(command, token);
        }

        [Fact]
        public async Task Handle_invalid()
        {
            var loggerMock = new Mock<ILogger<MoveUserToOrganizationCommandHandler>>();
            _context
                .Setup(x => x.UserRepository.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(User));

            MoveUserToOrganizationCommand command = new MoveUserToOrganizationCommand
            {
                OrganizationGuid = Guid.NewGuid(),
                UserGuid = Guid.NewGuid()
            };

            MoveUserToOrganizationCommandHandler handler = new MoveUserToOrganizationCommandHandler(_context.Object, loggerMock.Object);
            var token = new CancellationToken();
            await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(command, token));
        }
    }
}
