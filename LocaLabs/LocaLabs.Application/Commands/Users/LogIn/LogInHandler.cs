using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Users.LogIn
{
    internal class LogInHandler : Handler<LogInCmd, (Option<User> User, Option<Client> Client)>
    {
        public LogInHandler(INotificationService notifier, IEncryptService encrypt, IUserRepository userRepo, IClientRepository clientRepo)
            : base(notifier)
        {
            Encrypt = encrypt;
            UserRepo = userRepo;
            ClientRepo = clientRepo;
        }

        public IEncryptService Encrypt { get; }
        public IUserRepository UserRepo { get; }
        public IClientRepository ClientRepo { get; }

        protected override async Task<(Option<User> User, Option<Client> Client)> Perform(LogInCmd request, CancellationToken cancellationToken)
        {
            var login = request.Login;
            var possibleCpf = (Cpf)request.Login;

            if (possibleCpf.IsValid)
                login = possibleCpf.Unformated;

            var passwordHash = Encrypt.GetHashFrom(request.Password);
            var hasUser = await UserRepo.FindUserByLogin(login, passwordHash, cancellationToken);

            if (hasUser.IsNone)
                return (Option<User>.None, Option<Client>.None);

            if (!possibleCpf.IsValid)
                return (hasUser, Option<Client>.None);

            var hasClient = await ClientRepo.FindByCpf(possibleCpf, cancellationToken);
            return (hasUser, hasClient);
        }
    }
}