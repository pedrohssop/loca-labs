using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Cars.CreateCar
{
    class CreateCarHandler : Handler<CreateCarCmd, Car>
    {
        public CreateCarHandler(INotificationService notifier, ICarRepository carRepo)
            : base(notifier)
        {
            CarRepo = carRepo;
        }

        public ICarRepository CarRepo { get; }

        protected override async Task<Car> Perform(CreateCarCmd cmd, CancellationToken cancellationToken)
        {
            var existsThisCar = await CarRepo.FindByPlate(cmd.Plate, cancellationToken);
            if (existsThisCar.IsSome)
                return (Car)existsThisCar;

            var newCar = new Car(cmd.Plate, cmd.Fuel, cmd.BradId, cmd.HourValue, cmd.TrunkVolume, cmd.Category);
            await CarRepo.AddCar(newCar, cancellationToken);

            return newCar;
        }
    }
}
