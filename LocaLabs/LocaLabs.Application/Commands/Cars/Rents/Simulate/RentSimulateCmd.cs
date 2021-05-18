using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using System;

namespace LocaLabs.Application.Commands.Cars.Rents.Simulate
{
    public class RentSimulateCmd : Command<Option<Rent>>
    {
        public Guid CarId { get; init; }
        public DateTime End { get; init; }
        public DateTime Start { get; init; }
    }
}