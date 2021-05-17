using LocaLabs.Domain.Entities;
using MediatR;

namespace LocaLabs.Application.Commands.Clients.CreateClient
{
    public record ClientCreationNotification(Client client) : INotification
    {
    }
}