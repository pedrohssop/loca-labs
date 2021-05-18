using FluentAssertions;
using LanguageExt;
using LocaLabs.Application.Commands.Cars.Rents.Schedule;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Cars.Rents;
using LocaLabs.Domain.Enums;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LocaLabs.Tests.Application.Cars.Rents
{
    [Trait("Application Commands", "Schedule Rent")]
    public class ScheduleCase : HandlerCaseBase
    {
        [Fact(DisplayName = "Check when car does not exists")]
        public async Task ScheduleMissingCar()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var token = CancellationToken.None;
            var carRepo = new Mock<ICarRepository>();
            carRepo.Setup(s => s.FindById(carId, token)).Returns(ValueTask.FromResult(Option<Car>.None));

            var command = new ScheduleRentCmd
            {
                CarId = carId,
                End = DateTime.UtcNow,
                Start = DateTime.UtcNow
            };

            command.AssignClient(Guid.NewGuid());
            var handler = new ScheduleRentHandler(NotificationMoq(), carRepo.Object, null);

            // Act
            var rent = await handler.Handle(command, token);

            // Assert
            rent.Data.IsNone.Should().BeTrue("this car not exists");
            Notifications.Count.Should().Be(1, "this car not exists");
        }

        [Fact(DisplayName = "Check when car is not avaliable")]
        public async Task ScheduleUnavaliableCar()
        {
            // Arrange
            var token = CancellationToken.None;
            var car = new Car("example", FuelTypes.Flex, Guid.NewGuid(), 0M, 0, CarCategories.Basic);

            var carRepo = new Mock<ICarRepository>();
            carRepo.Setup(s => s.FindById(car.Id, token))
                   .Returns(ValueTask.FromResult((Option<Car>)car));

            var rentRepo = new Mock<IRentRepository>();
            rentRepo.Setup(s => s.IsCarAvaliableForRent(car.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>(), token))
                    .Returns(ValueTask.FromResult(false));

            var command = new ScheduleRentCmd
            {
                CarId = car.Id,
                End = DateTime.UtcNow,
                Start = DateTime.UtcNow
            };

            command.AssignClient(Guid.NewGuid());
            var handler = new ScheduleRentHandler(NotificationMoq(), carRepo.Object, rentRepo.Object);

            // Act
            var rent = await handler.Handle(command, token);

            // Assert
            rent.Data.IsNone.Should().BeTrue("this car is not avaliable.");
            Notifications.Count.Should().Be(1, "this car is not avaliable.");
        }

        [Fact(DisplayName = "Check rent entity creation")]
        public async Task ScheduleChechRentEntityIntegrity()
        {
            // Arrange
            var token = CancellationToken.None;
            var car = new Car("example", FuelTypes.Flex, Guid.NewGuid(), 0M, 0, CarCategories.Basic);

            var carRepo = new Mock<ICarRepository>();
            carRepo.Setup(s => s.FindById(car.Id, token))
                   .Returns(ValueTask.FromResult((Option<Car>)car));

            var rentRepo = new Mock<IRentRepository>();
            rentRepo.Setup(s => s.IsCarAvaliableForRent(car.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>(), token))
                    .Returns(ValueTask.FromResult(true));

            Rent savedRent = null;
            rentRepo.Setup(s => s.AddRent(It.IsAny<Rent>(), token))
                    .Callback<Rent, CancellationToken>((r,c) => savedRent = r);

            var command = new ScheduleRentCmd
            {
                CarId = car.Id,
                End = DateTime.UtcNow,
                Start = DateTime.UtcNow
            };

            command.AssignClient(Guid.NewGuid());
            var handler = new ScheduleRentHandler(NotificationMoq(), carRepo.Object, rentRepo.Object);

            // Act
            var rent = await handler.Handle(command, token);

            // Assert
            Notifications.Count.Should().Be(0, "it's all ok.");
            rent.Data.IsSome.Should().BeTrue("the car must be rented.");

            savedRent.Should().NotBeNull();
            savedRent.CarId.Should().Be(car.Id);
            savedRent.End.Should().Be(command.End);
            savedRent.Start.Should().Be(command.Start);
            savedRent.ClientId.Should().Be(command.ClientId);
        }
    }
}