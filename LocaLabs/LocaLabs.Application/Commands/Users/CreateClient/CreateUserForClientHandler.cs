using LocaLabs.Application.Commands.Clients.CreateClient;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Users.CreateClient
{
    internal class CreateUserForClientHandler
        : INotificationHandler<ClientCreationNotification>
    {
        const string FirstAccess = "primeiro_acesso";

        public CreateUserForClientHandler(IUserRepository userRepo, IEncryptService encrypt)
        {
            UserRepo = userRepo;
            Encrypt = encrypt;
        }

        IEncryptService Encrypt { get; }
        IUserRepository UserRepo { get; }

        public async Task Handle(ClientCreationNotification ntf, CancellationToken cancellationToken)
        {
            var hash = Encrypt.GetHashFrom(FirstAccess);
            await UserRepo.AddClient(ntf.client.Cpf, hash, cancellationToken);
        }
    }
}