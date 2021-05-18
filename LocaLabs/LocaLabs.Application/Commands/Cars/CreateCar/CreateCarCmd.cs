using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LocaLabs.Application.Commands.Cars.CreateCar
{
    public class CreateCarCmd : Command<Car>
    {
        public Guid BradId { get; init; }
        public string Plate { get; init; }
        public decimal HourValue { get; init; }
        public short TrunkVolume { get; init; }
        public FuelTypes Fuel { get; init; } = FuelTypes.Gasoline;
        public CarCategories Category { get; init; } = CarCategories.Basic;

        internal override IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(Plate))
                yield return "Plate is required";
        }
    }
}