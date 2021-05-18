using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Cars.CreateBrand
{
    class CreateBrandHandler : Handler<CreateBrandCmd, CarBrand>
    {
        public CreateBrandHandler(INotificationService notifier, ICarRepository carRepo)
            : base(notifier)
        {
            CarRepo = carRepo;
        }

        public ICarRepository CarRepo { get; }

        protected override async Task<CarBrand> Perform(CreateBrandCmd request, CancellationToken cancellationToken)
        {
            var brand = new CarBrand(request.Year, request.Model, request.Brand);
            var existsThisBrand = await CarRepo.IsBrandUnique(brand, cancellationToken);

            if (existsThisBrand.IsNone)
            {
                await CarRepo.AddBrand(brand, cancellationToken);
                return brand;
            }

            else return (CarBrand)existsThisBrand;
        }
    }
}
