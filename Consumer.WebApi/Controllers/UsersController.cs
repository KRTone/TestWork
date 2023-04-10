using Consumer.Application.Commands;
using Consumer.Application.Queries;
using Consumer.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.WebApi.Controllers
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


        [HttpPost("MoveUserToOrganization")]
        public Task PostOrganization(MoveUserToOrganizationCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPost("GetPageByOrganization")]
        public Task<List<User>> GetPage(GetUsersQuery query)
        {
            return _mediator.Send(query);
        }
    }
}