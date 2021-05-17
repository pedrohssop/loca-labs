using LocaLabs.Application.Services;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Application.Commands.Base
{
    public abstract class Handler<T, U> : IRequestHandler<T, CommandOutput<U>>
        where T : Command<U>
    {
        private INotificationService Notifier { get; }

        public Handler(INotificationService notifier)
        {
            Notifier = notifier;
        }

        public void Notify(string message) =>
            Notifier.AddNotification(message);

        public async Task<CommandOutput<U>> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                var messages = request.Validate();
                if (messages.Any())
                {
                    Notifier.AddNotification(messages.ToArray());
                    return false;
                }

                return await Perform(request, cancellationToken);
            }
            catch (Exception ex)
            {
                Notifier.AddError(ex);
                return ex;
            }
        }

        protected abstract Task<U> Perform(T request, CancellationToken cancellationToken);
    }
}