using LocaLabs.Application.Commands.Base;
using System;

namespace LocaLabs.Application.Commands.Cars.Rents.GenerateLeaseAgreement
{
    public class LeaseAgreementCmd : Command<byte[]>
    {
        public Guid RentId { get; init; }
    }
}