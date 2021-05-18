using LocaLabs.Domain.Constants;
using LocaLabs.Domain.Entities.Base;
using LocaLabs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities
{
    public class Car : Entity
    {
        public string Plate { get; }
        public Guid BrandId { get; }
        public FuelTypes Fuel { get; }
        public CarBrand Brand { get; }
        public decimal HourValue { get; }
        public short TrunkVolume { get; }
        public CarCategories Category { get; }

        public Car(string plate, FuelTypes fuel, Guid brandId, decimal hourValue, short trunkVolume, CarCategories category)
        {
            Fuel = fuel;
            Plate = plate;
            BrandId = brandId;
            Category = category;
            HourValue = hourValue;
            TrunkVolume = trunkVolume;
        }

        public Car(string plate, FuelTypes fuel, CarBrand brand, decimal hourValue, short trunkVolume, CarCategories category)
        {
            Fuel = fuel;
            Plate = plate;
            Brand = brand;
            BrandId = brand.Id;
            Category = category;
            HourValue = hourValue;
            TrunkVolume = trunkVolume;
        }

        public Car(string plate, FuelTypes fuel, CarBrand brand, Guid brandId, decimal hourValue, short trunkVolume, CarCategories category)
        {
            Fuel = fuel;
            Plate = plate;
            Brand = brand;
            BrandId = brandId;
            Category = category;
            HourValue = hourValue;
            TrunkVolume = trunkVolume;
        }

        public Car(Guid id, DateTime createdAt, string plate, FuelTypes fuel, CarBrand brand, Guid brandId, decimal hourValue, short trunkVolume, CarCategories category)
            : base(id, createdAt)
        {
            Fuel = fuel;
            Plate = plate;
            Brand = brand;
            BrandId = brandId;
            Category = category;
            HourValue = hourValue;
            TrunkVolume = trunkVolume;
        }

        public Rent Rent(Guid clientId, DateTime start, DateTime end) =>
            new Rent(this, clientId, start, end);

        public override IEnumerable<EntityValidation> IsValid()
        {
            if (string.IsNullOrEmpty(Plate))
                yield return new EntityValidation(EntityValidations.Required, nameof(Plate));

            if (HourValue == 0)
                yield return new EntityValidation(EntityValidations.Required, nameof(HourValue));

            if (TrunkVolume == 0)
                yield return new EntityValidation(EntityValidations.Required, nameof(TrunkVolume));
        }
    }
}