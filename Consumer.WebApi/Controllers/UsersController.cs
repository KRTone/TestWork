using AutoMapper;
using Consumer.Application.Commands;
using Consumer.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost("MoveUserToOrganization")]
        public Task PostOrganization(MoveUserToOrganizationCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPost("GetPageByOrganization")]
        public async Task<List<ViewModels.User>> GetPage(GetUsersQuery query)
        {
            var users = await _mediator.Send(query);
            return _mapper.Map<List<ViewModels.User>>(users);
        }
    }
}