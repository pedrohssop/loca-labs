using FluentAssertions;
using LanguageExt;
using LocaLabs.Application.Commands.Cars.Rents.Schedule;
using LocaLabs.Domain.Entities;
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
    }
}