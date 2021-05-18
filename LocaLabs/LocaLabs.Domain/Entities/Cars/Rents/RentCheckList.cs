using LocaLabs.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocaLabs.Domain.Entities
{
    public class RentCheckList : Entity
    {
        public Guid RentId { get; }
        public bool Full { get; }
        public bool Clean { get; }
        public bool WithoutScratches { get; }

        internal RentCheckList(Guid rentId, bool full, bool clean, bool withoutScratches)
        {
            Full = full;
            Clean = clean;
            RentId = rentId;
            WithoutScratches = withoutScratches;
        }

        public RentCheckList(Guid id, DateTime createdAt, Guid rentId, bool full, bool clean, bool withoutScratches)
            : base(id, createdAt)
        {
            Full = full;
            Clean = clean;
            RentId = rentId;
            WithoutScratches = withoutScratches;
        }

        public override IEnumerable<EntityValidation> IsValid() =>
            Enumerable.Empty<EntityValidation>();
    }
}