using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using System;

namespace LocaLabs.Application.Commands.Cars.Rents.RegisterCheckList
{
    public class RegisterCheckListCmd : Command<Option<Rent>>
    {
        public bool Full { get; init; }
        public bool Clean { get; init; }
        public Guid RentId { get; init; }
        public bool GoodState { get; init; }
    }
}