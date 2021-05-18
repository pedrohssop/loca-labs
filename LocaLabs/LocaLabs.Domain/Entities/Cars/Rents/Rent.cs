using LocaLabs.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities
{
    public class Rent : Entity
    {
        public Car Car { get; }
        public Guid CarId { get; }
        public Guid ClientId { get; }
        public DateTime End { get; }
        public DateTime Start { get; }
        public decimal Cost { get; private set; }
        public bool Completed { get; private set; }

        internal Rent(Car car, Guid clientId, DateTime start, DateTime end)
        {
            Car = car;
            End = end;
            Start = start;
            CarId = car.Id;
            Completed = false;
            ClientId = clientId;
            Cost = TotalHours * Car.HourValue;
        }

        public Rent(Guid id, Guid carId, Guid clientId, DateTime createdAt, DateTime start, DateTime end, bool completed, decimal cost)
            : base(id, createdAt)
        {
            End = end;
            Cost = cost;
            CarId = carId;
            Start = start;
            ClientId = clientId;
            Completed = completed;
        }

        public int TotalHours =>
            (int)Math.Floor((End - Start).TotalHours);

        public override IEnumerable<EntityValidation> IsValid()
        {
            if (Start >= End)
                yield return new EntityValidation("Start date must be greater than end date.", "Start and End");
        }

        public RentCheckList Check(bool clean, bool full, bool goodState)
        {
            var tax = Cost * 0.3M;

            if (!full) Cost += tax;
            if (!clean) Cost += tax;
            if (!goodState) Cost += tax;

            Completed = true;

            return new RentCheckList(Id, full, clean, goodState);
        }
    }
}
