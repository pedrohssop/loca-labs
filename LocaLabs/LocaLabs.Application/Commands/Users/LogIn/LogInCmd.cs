using LanguageExt;
using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using System.Collections.Generic;

namespace LocaLabs.Application.Commands.Users.LogIn
{
    public class LogInCmd : Command<(Option<User> User, Option<Client> Client)>
    {
        public string Login { get; init; }
        public string Password { get; init; }

        internal override IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(Login))
                yield return "Login is required";

            if (string.IsNullOrEmpty(Password))
                yield return "Password is required";
        }
    }
}