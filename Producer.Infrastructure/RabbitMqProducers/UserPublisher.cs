using AutoMapper;
using MassTransit;
using Producer.Domain.Aggregates.UserAggregate;
using Producer.Domain.Interfaces;
using UserDtos;

namespace Producer.Infrastructure.RabbitMqProducers
{
    public class UserPublisher : IPublisher<User>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public UserPublisher(IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task PublishAsync(User user, CancellationToken token = default)
        {
            CreateUser dto = _mapper.Map<CreateUser>(user);
            await _publishEndpoint.Publish(dto, token);
        }
    }
}