using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Cars.Rents.Simulate
{
    class RentSimulateHandler : Handler<RentSimulateCmd, Option<Rent>>
    {
        public RentSimulateHandler(INotificationService notifier, ICarRepository carRepo)
            : base(notifier)
        {
            CarRepo = carRepo;
        }

        public ICarRepository CarRepo { get; }

        protected override async Task<Option<Rent>> Perform(RentSimulateCmd request, CancellationToken cancellationToken)
        {
            var hasCar = await CarRepo.FindById(request.CarId, cancellationToken);
            if (hasCar.IsNone)
            {
                Notify($"The car {request.CarId} does not exists.");
                return Option<Rent>.None;
            }

            var car = (Car)hasCar;
            var dummyClientId = Guid.NewGuid();

            var rent = car.Rent(dummyClientId, request.Start, request.End);
            if (rent.IsValid().Any())
            {
                NotifyRentValidations(rent);
                return Option<Rent>.None;
            }

            return rent;
        }

        private void NotifyRentValidations(Rent rent)
        {
            foreach (var rentCheck in rent.IsValid())
                Notifier.AddNotification(rentCheck);
        }
    }
}
