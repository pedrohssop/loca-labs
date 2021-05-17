using LocaLabs.Application.Commands.Base;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Users.CreateOperator
{
    internal class CreateOpUserHandler : Handler<CreateOpUserCmd, User>
    {
        public CreateOpUserHandler(INotificationService notifier, IEncryptService encrypt, IUserRepository userRepo)
            : base(notifier)
        {
            Encrypt = encrypt;
            UserRepo = userRepo;
        }

        public IEncryptService Encrypt { get; }
        public IUserRepository UserRepo { get; }

        protected override async Task<User> Perform(CreateOpUserCmd request, CancellationToken cancellationToken)
        {
            var hash = Encrypt.GetHashFrom(request.Password);
            var newUser = await UserRepo.AddOperator(request.Login, hash, cancellationToken);

            return newUser;
        }
    }
}