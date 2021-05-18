using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Abstractions;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Cars.Rents;
using LocaLabs.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Cars.Rents.GenerateLeaseAgreement
{
    public class LeaseAgreementHandler : Handler<LeaseAgreementCmd, byte[]>
    {
        public LeaseAgreementHandler(
            INotificationService notifier,
            ICarRepository carRepo,
            IRentRepository rentRepo,
            ITemplateGenerator template,
            IClientRepository clientRepo,
            IFileService file) : base(notifier)
        {
            File = file;
            CarRepo = carRepo;
            RentRepo = rentRepo;
            Template = template;
            ClientRepo = clientRepo;
        }

        public IFileService File { get; }
        public ICarRepository CarRepo { get; }
        public IRentRepository RentRepo { get; }
        public ITemplateGenerator Template { get; }
        public IClientRepository ClientRepo { get; }

        protected override async Task<byte[]> Perform(LeaseAgreementCmd request, CancellationToken cancellationToken)
        {
            var hasRent = await RentRepo.FindRentById(request.RentId, cancellationToken);
            if (hasRent.IsNone)
            {
                Notify("The rent does not exists");
                return Array.Empty<byte>();
            }

            var rent = (Rent)hasRent;
            var car = (Car)await CarRepo.FindById(rent.Id, cancellationToken);
            var client = (Client)await ClientRepo.FindById(rent.ClientId, cancellationToken);

            var fileContent = Template.Generate(Templates.LeaseAgreement, new
            {
                Name = client.Name,
                Address = client.City,
                Cpf = client.Cpf.Formated,
                Brand = car.Brand.Brand,
                Model = car.Brand.Model,
                Year = car.Brand.Year,
                TotalValue = rent.Cost,
                Start = rent.Start,
                End = rent.End
            });

            return File.AsPdf(fileContent);
        }
    }
}
