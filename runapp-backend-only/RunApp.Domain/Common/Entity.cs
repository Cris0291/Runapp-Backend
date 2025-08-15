using System.Collections.Immutable;

namespace RunApp.Domain.Common
{
    public abstract class Entity : ValidationHandler
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        protected void RaiseEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IReadOnlyCollection<IDomainEvent> GetEvents()
        {
            var temp = _domainEvents.ToList();
            _domainEvents.Clear();
            return temp;
        }
    }
}
