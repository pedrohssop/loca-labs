using FluentAssertions;
using LanguageExt;
using LocaLabs.Application.Commands.Cars.Rents.RegisterCheckList;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Cars.Rents;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LocaLabs.Tests.Application.Cars.Rents
{
    [Trait("Application Commands", "Register Check List")]
    public class RegisterCheckList : HandlerCaseBase
    {
        [Fact(DisplayName = "Check when rent does not exists")]
        public async Task CheckRentDoesNotExists()
        {
            // Arrange
            var rentId = Guid.NewGuid();
            var token = CancellationToken.None;

            var rentRepo = new Mock<IRentRepository>();
            rentRepo.Setup(s => s.FindRentById(rentId, token))
                   .Returns(ValueTask.FromResult(Option<Rent>.None));

            var command = new RegisterCheckListCmd { RentId = rentId };
            var handler = new RegisterCheckListHandler(NotificationMoq(), rentRepo.Object);

            // Act
            var rent = await handler.Handle(command, token);

            // Assert
            rent.Data.IsNone.Should().BeTrue("this rent not exists");
            Notifications.Count.Should().Be(1, "this rent not exists");
        }

        [Fact(DisplayName = "Check when rent it's already completed")]
        public async Task CheckCompletedRent()
        {
            // Arrange
            var rent = CreateRent(true);
            var token = CancellationToken.None;

            var rentRepo = new Mock<IRentRepository>();
            rentRepo.Setup(s => s.FindRentById(rent.Id, token))
                    .Returns(ValueTask.FromResult((Option<Rent>)rent));

            var command = new RegisterCheckListCmd { RentId = rent.Id };
            var handler = new RegisterCheckListHandler(NotificationMoq(), rentRepo.Object);

            // Act
            var resutl = await handler.Handle(command, token);

            // Assert
            resutl.Data.IsNone.Should().BeTrue("this rent it's completed.");
            Notifications.Count.Should().Be(1, "this rent it's completed.");
        }

        [Fact(DisplayName = "Check rent and check list entities")]
        public async Task CheckSavedEntities()
        {
            // Arrange
            var rent = CreateRent(false);
            var token = CancellationToken.None;

            var rentRepo = new Mock<IRentRepository>();
            rentRepo.Setup(s => s.FindRentById(rent.Id, token))
                    .Returns(ValueTask.FromResult((Option<Rent>)rent));

            bool rentWasUpdated = false;
            rentRepo.Setup(s => s.UpdateRent(It.IsAny<Rent>(), token))
                    .Callback<Rent, CancellationToken>((r, c) => rentWasUpdated = true);

            RentCheckList savedCheck = null;
            rentRepo.Setup(s => s.AddCheckList(It.IsAny<RentCheckList>(), token))
                    .Callback<RentCheckList, CancellationToken>((r, c) => savedCheck = r);

            var command = new RegisterCheckListCmd { RentId = rent.Id };
            var handler = new RegisterCheckListHandler(NotificationMoq(), rentRepo.Object);

            // Act
            var resutl = await handler.Handle(command, token);

            // Assert
            Notifications.Should().HaveCount(0);
            resutl.Data.IsSome.Should().BeTrue();

            rentWasUpdated.Should().BeTrue();
            rent.Completed.Should().BeTrue();
            savedCheck.Should().NotBeNull();
        }

        private static Rent CreateRent(bool isCompleted) =>
            new Rent
            (
                id: Guid.NewGuid(),
                carId: Guid.NewGuid(),
                clientId: Guid.NewGuid(),
                createdAt: DateTime.UtcNow,
                start: DateTime.UtcNow,
                end: DateTime.UtcNow,
                completed: isCompleted,
                cost: 100M
            );
    }
}