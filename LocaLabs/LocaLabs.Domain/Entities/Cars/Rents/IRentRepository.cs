using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Domain.Entities.Cars.Rents
{
    public interface IRentRepository
    {
        ValueTask AddRent(Rent rent, CancellationToken cancel);
        ValueTask UpdateRent(Rent rent, CancellationToken cancel);
        ValueTask AddCheckList(RentCheckList checkList, CancellationToken cancel);
        ValueTask<Option<Rent>> FindRentById(Guid rentId, CancellationToken cancel);
        ValueTask<bool> IsCarAvaliableForRent(Guid carId, DateTime start, DateTime end, CancellationToken cancel);
    }
}
