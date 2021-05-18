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
        public bool GoodState { get; }

        internal RentCheckList(Guid rentId, bool full, bool clean, bool goodState)
        {
            Full = full;
            Clean = clean;
            RentId = rentId;
            GoodState = goodState;
        }

        public RentCheckList(Guid id, DateTime createdAt, Guid rentId, bool full, bool clean, bool goodState)
            : base(id, createdAt)
        {
            Full = full;
            Clean = clean;
            RentId = rentId;
            GoodState = goodState;
        }

        public override IEnumerable<EntityValidation> IsValid() =>
            Enumerable.Empty<EntityValidation>();
    }
}