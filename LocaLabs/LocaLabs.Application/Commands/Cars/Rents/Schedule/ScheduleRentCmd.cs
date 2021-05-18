using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using System;

namespace LocaLabs.Application.Commands.Cars.Rents.Schedule
{
    public class ScheduleRentCmd : Command<Option<Rent>>
    {
        public Guid CarId { get; init; }
        public DateTime End { get; init; }
        public DateTime Start { get; init; }
        public Guid ClientId { get; private set; }

        public void AssignClient(Guid id) =>
            ClientId = id;
    }
}