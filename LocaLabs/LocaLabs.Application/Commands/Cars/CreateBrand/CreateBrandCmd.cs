using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;

namespace LocaLabs.Application.Commands.Cars.CreateBrand
{
    public class CreateBrandCmd : Command<CarBrand>
    {
        public short Year { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
    }
}