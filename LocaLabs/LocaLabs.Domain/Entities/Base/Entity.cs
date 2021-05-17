using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities.Base
{
    public abstract class Entity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public bool IsNew { get; private set; }

        public Entity(Guid id, DateTime createdAt)
        {
            Id = id;
            IsNew = false;
            CreatedAt = createdAt;
        }

        public Entity()
        {
            IsNew = true;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public void Persisted() =>
            IsNew = false;

        /// <summary>
        /// Check if entity is valid to persist
        /// </summary>
        /// <returns>A collection with validations by field. Empty if is valid</returns>
        public abstract IEnumerable<EntityValidation> IsValid();
    }
}
