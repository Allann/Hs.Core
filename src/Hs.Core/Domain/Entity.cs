using Hs.Core.Concepts;
using MediatR;
using System;
using System.Collections.Generic;

namespace Hs.Core.Domain
{
    public abstract class Entity<TKey> 
        where TKey : IEquatable<TKey>
    {
        private int? _requestedHashCode;

        #region Domain Events

        private List<INotification>? _domainEvents;
        public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion

        public virtual TKey Id { get; protected set; } = default!;

        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default);
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TKey> item
                && (ReferenceEquals(this, item)
                || !item.IsTransient() && !IsTransient() && EqualityComparer<TKey>.Default.Equals(Id, item.Id));
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = HashCodeHelper.Generate(Id);
                }
                return _requestedHashCode.Value;
            }
            return base.GetHashCode();
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }

    public abstract class Entity : Entity<int>
    { }
}
