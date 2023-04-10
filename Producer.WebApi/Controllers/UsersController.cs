using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producer.Application.Commands;

namespace Producer.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<Guid> Create(CreateUserCommand command)
        {
            return _mediator.Send(command);
        }
    }
}