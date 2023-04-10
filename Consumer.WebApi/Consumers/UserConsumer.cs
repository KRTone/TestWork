using AutoMapper;
using Consumer.Application.Commands;
using MassTransit;
using MediatR;
using UserDtos;

namespace Consumer.WebApi.Consumers
{
    public class UserConsumer : IConsumer<CreateUser>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<CreateUser> context)
        {
            CreateUserCommand command = _mapper.Map<CreateUserCommand>(context.Message);
            return _mediator.Send(command);
        }
    }
}