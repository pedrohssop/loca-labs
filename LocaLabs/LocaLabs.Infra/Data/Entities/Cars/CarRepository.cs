using LanguageExt;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Infra.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Infra.Data.Entities.Cars
{
    internal class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(INotificationService notifier, DataContext ctx)
            : base(notifier, ctx) { }

        public async ValueTask AddBrand(CarBrand brand, CancellationToken cancel)
        {
            if (CheckEntity(brand))
                await SaveEntity(brand, cancel);
        }

        public async ValueTask AddCar(Car car, CancellationToken cancel)
        {
            if (CheckEntity(car))
                await SaveEntity(car, cancel);
        }

        public ValueTask<IEnumerable<Car>> All(CancellationToken cancel) =>
            ValueTask.FromResult(Ctx.Cars.AsEnumerable());

        public async ValueTask<Option<Car>> FindById(Guid id, CancellationToken cancel) =>
            await Ctx.Cars.FindAsync(id, cancel);

        public async ValueTask<Option<Car>> FindByPlate(string plate, CancellationToken cancel) =>
            await Ctx.Cars.Where(w => w.Plate == plate).FirstOrDefaultAsync(cancel);

        public async ValueTask<Option<CarBrand>> IsBrandUnique(CarBrand brand, CancellationToken cancel) =>
            await Ctx.Brands.Where(w => w.Model == brand.Model && w.Year == brand.Year && w.Brand == brand.Brand).FirstOrDefaultAsync(cancel);
    }
}