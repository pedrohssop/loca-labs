using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Clients.CreateClient
{
    internal class CreateClientHandler : Handler<CreateClientCmd, Client>
    {
        public CreateClientHandler(INotificationService notifier, IClientRepository clientRepo, IMediator mediator)
            : base(notifier)
        {
            ClientRepo = clientRepo;
            Mediator = mediator;
        }

        public IMediator Mediator { get; }
        public IClientRepository ClientRepo { get; }

        protected override async Task<Client> Perform(CreateClientCmd r, CancellationToken cancellationToken)
        {
            var address = new Address(r.Cep, r.Street, r.Number, r.City, r.State, r.Complement);
            var client = new Client(r.Cpf, r.Name, r.Dob, address);

            await ClientRepo.AddClient(client, cancellationToken);

            if (!client.IsNew)
                await Mediator.Publish(new ClientCreationNotification(client));

            return client;
        }
    }
}
