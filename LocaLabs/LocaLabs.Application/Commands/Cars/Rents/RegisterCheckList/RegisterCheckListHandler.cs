using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Cars.Rents;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Cars.Rents.RegisterCheckList
{
    internal class RegisterCheckListHandler : Handler<RegisterCheckListCmd, Option<Rent>>
    {
        public RegisterCheckListHandler(INotificationService notifier, IRentRepository rentRepo)
            : base(notifier)
        {
            RentRepo = rentRepo;
        }

        public IRentRepository RentRepo { get; }

        protected override async Task<Option<Rent>> Perform(RegisterCheckListCmd request, CancellationToken cancellationToken)
        {
            var hasRent = await RentRepo.FindRentById(request.RentId, cancellationToken);
            if (hasRent.IsNone)
            {
                Notify($"The Rent {request.RentId} does not exists.");
                return Option<Rent>.None;
            }

            var rent = (Rent)hasRent;
            if (rent.Completed)
            {
                Notify($"The Rent {request.RentId} is already completed.");
                return Option<Rent>.None;
            }

            var rentChech = rent.Check(request.Clean, request.Full, request.GoodState);

            await RentRepo.AddCheckList(rentChech, cancellationToken);
            await RentRepo.UpdateRent(rent, cancellationToken);

            return rent;
        }
    }
}