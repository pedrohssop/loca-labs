using LanguageExt;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Domain.Entities
{
    public interface ICarRepository
    {
        ValueTask AddCar(Car car, CancellationToken cancel);
        ValueTask AddBrand(CarBrand brand, CancellationToken cancel);

        ValueTask<IEnumerable<Car>> All(CancellationToken cancel);
        ValueTask<Option<Car>> FindById(Guid id, CancellationToken cancel);
        ValueTask<Option<Car>> FindByPlate(string plate, CancellationToken cancel);
        ValueTask<Option<CarBrand>> IsBrandUnique(CarBrand brand, CancellationToken cancel);
    }
}
