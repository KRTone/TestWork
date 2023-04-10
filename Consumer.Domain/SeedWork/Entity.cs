using MediatR;

namespace Consumer.Domain.SeedWork
{
    public abstract class Entity
    {
        Guid _guid;
        public virtual Guid Guid
        {
            get => _guid;
            protected set => _guid = value;
        }

        private List<INotification> _domainEvents = new List<INotification>();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient() => Guid == default;

        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
