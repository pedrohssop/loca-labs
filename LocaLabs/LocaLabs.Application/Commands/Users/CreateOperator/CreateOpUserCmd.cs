using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using System.Collections.Generic;

namespace LocaLabs.Application.Commands.Users.CreateOperator
{
    public class CreateOpUserCmd : Command<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        internal override IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(Password))
                yield return "Password is required";
        }
    }
}