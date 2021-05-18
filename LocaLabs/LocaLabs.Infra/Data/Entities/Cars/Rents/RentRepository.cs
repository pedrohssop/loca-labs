using LanguageExt;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Cars.Rents;
using LocaLabs.Infra.Data.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Infra.Data.Entities.Cars.Rents
{
    internal class RentRepository : BaseRepository<Rent>, IRentRepository
    {
        public RentRepository(INotificationService notifier, DataContext ctx)
            : base(notifier, ctx)
        {
        }

        public ValueTask AddCheckList(RentCheckList checkList, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public ValueTask AddRent(Rent rent, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Option<Rent>> FindRentById(Guid rentId, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> IsCarAvaliableForRent(Guid carId, DateTime start, DateTime end, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public ValueTask UpdateRent(Rent rent, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
    }
}