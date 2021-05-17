using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace LocaLabs.Application.Commands.Base
{
    public abstract class Command<T> : IRequest<CommandOutput<T>>
    {
        internal virtual IEnumerable<string> Validate() =>
            Enumerable.Empty<string>();
    }
}