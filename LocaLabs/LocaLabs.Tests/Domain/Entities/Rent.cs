using FluentAssertions;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Enums;
using System;
using System.Linq;
using Xunit;

namespace LocaLabs.Tests.Domain.Entities
{
    [Trait("Entities Validation", "Rent")]
    public class RentCase
    {
        [Fact(DisplayName = "Star date bigger than end one")]
        public void CheckDatesValidation()
        {
            var rent = new Rent
            (
                id: Guid.NewGuid(),
                carId: Guid.NewGuid(),
                clientId: Guid.NewGuid(),
                createdAt: DateTime.UtcNow,
                start: DateTime.UtcNow.AddDays(1),
                end: DateTime.UtcNow,
                completed: true,
                cost: 100M
            );

            rent.IsValid().Should().HaveCount(1);
            rent.IsValid().ElementAt(0).Field.Should().Be("Start and End");
        }

        [Fact(DisplayName = "Check cost calc")]
        public void CheckCostCalc()
        {
            var car = new Car("example", FuelTypes.Flex, Guid.NewGuid(), 100M, 0, CarCategories.Basic);
            var rent = new Rent
            (
                car: car,
                clientId: Guid.NewGuid(),
                start: DateTime.UtcNow.AddDays(-1),
                end: DateTime.UtcNow
            );

            rent.IsValid().Should().HaveCount(0);
            rent.Completed.Should().BeFalse();
            rent.CarId.Should().Be(car.Id);
            rent.Cost.Should().Be(100 * 24, "the rent has 24 hours and the car hour cost is 100");
        }

        [Fact(DisplayName = "Validate rent check list registration")]
        public void ValidateCheckRent()
        {
            var rent = new Rent
            (
                id: Guid.NewGuid(),
                carId: Guid.NewGuid(),
                clientId: Guid.NewGuid(),
                createdAt: DateTime.UtcNow,
                start: DateTime.UtcNow.AddDays(1),
                end: DateTime.UtcNow,
                completed: true,
                cost: 100M
            );

            var checkList = rent.Check(false, false, false);

            rent.Cost.Should().Be(190);
            rent.Completed.Should().BeTrue();

            checkList.Full.Should().BeFalse();
            checkList.Clean.Should().BeFalse();
            checkList.GoodState.Should().BeFalse();
        }
    }
}