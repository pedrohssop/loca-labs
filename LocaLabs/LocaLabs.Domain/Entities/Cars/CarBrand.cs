using LocaLabs.Domain.Constants;
using LocaLabs.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities
{
    public class CarBrand : Entity
    {
        public short Year { get; }
        public string Brand { get; }
        public string Model { get; }

        public CarBrand(short year, string model, string brand)
        {
            Year = year;
            Model = model;
            Brand = brand;
        }

        public CarBrand(Guid id, DateTime createdAt, short year, string model, string brand)
            : base(id, createdAt)
        {
            Year = year;
            Model = model;
            Brand = brand;
        }

        public override IEnumerable<EntityValidation> IsValid()
        {
            if (string.IsNullOrEmpty(Brand))
                yield return new EntityValidation(EntityValidations.Required, nameof(Brand));

            if (string.IsNullOrEmpty(Model))
                yield return new EntityValidation(EntityValidations.Required, nameof(Brand));
        }
    }
}