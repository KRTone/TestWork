using MediatR;
using System.Text;

namespace Producer.Domain.SeedWork
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
            IEnumerable<(string Name, object Value)> properties = GetType()
                .GetProperties()
                .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"));

            StringBuilder builder = new();
            builder.Append("[");
            builder.Append(properties);
            builder.Append("]");
            return builder.ToString();
        }
    }
}
