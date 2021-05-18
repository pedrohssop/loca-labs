using System;

namespace LocaLabs.Domain.Enums
{
    [Flags]
    public enum FuelTypes : byte
    {
        Gasoline = 1,
        Alcoll = 2,
        Flex = 3,
        Gas = 4,
        Oil = 8,
    }
}