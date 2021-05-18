using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Cars.Rents;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Cars.Rents.Schedule
{
    public class ScheduleRentHandler : Handler<ScheduleRentCmd, Option<Rent>>
    {
        public ScheduleRentHandler(INotificationService notifier, ICarRepository carRepo, IRentRepository rentRepo)
            : base(notifier)
        {
            CarRepo = carRepo;
            RentRepo = rentRepo;
        }

        ICarRepository CarRepo { get; }
        IRentRepository RentRepo { get; }

        protected override async Task<Option<Rent>> Perform(ScheduleRentCmd request, CancellationToken cancellationToken)
        {
            var hasCar = await CarRepo.FindById(request.CarId, cancellationToken);
            if (hasCar.IsNone)
                return InvalidRequest($"The car {request.CarId} does not exists.");

            var car = (Car)hasCar;
            var isAvaliable = await RentRepo.IsCarAvaliableForRent(car.Id, request.Start, request.End, cancellationToken);

            if (!isAvaliable)
                return InvalidRequest($"The car {request.CarId} is not avaliable for these dates.");

            var rent = car.Rent(request.ClientId, request.Start, request.End);
            await RentRepo.AddRent(rent, cancellationToken);

            return rent;
        }

        private Option<Rent> InvalidRequest(string message)
        {
            Notify(message);
            return Option<Rent>.None;
        }
    }
}