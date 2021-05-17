using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Base;
using LocaLabs.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LocaLabs.Tests.Domain.Entities
{
    [Trait("Entities Validation", "User")]
    public class UserCase
    {
        private const string Login = "loca";
        private const string Password = "Localiza_S2";

        private static List<User> InvalidUsers =
            new List<User>
            {
                new User(string.Empty, Password, UserProfiles.Operator),
                new User(null, Password, UserProfiles.Operator),
                new User("   ", Password, UserProfiles.Operator),
                new User(Login, string.Empty, UserProfiles.Operator),
                new User(Login, null, UserProfiles.Operator),
                new User(Login, "   ", UserProfiles.Operator),
            };

        [Fact(DisplayName = "User Validation")]
        public void CheckUserValidation() =>
            Assert.True(InvalidUsers.All(a => a.IsValid().Any()));

        [Fact(DisplayName = "User Password Check")]
        public void CheckUserPassword()
        {
            bool IsPasswordValidation(IEnumerable<EntityValidation> validations) =>
                validations.Count() == 1 && validations.ElementAt(0).Field == nameof(Password);

            var nullPassword = InvalidUsers[4];
            var emptyPassword = InvalidUsers[3];
            var spacePassword = InvalidUsers[5];

            Assert.True(IsPasswordValidation(nullPassword.IsValid()));
            Assert.True(IsPasswordValidation(emptyPassword.IsValid()));
            Assert.True(IsPasswordValidation(spacePassword.IsValid()));
        }

        [Fact(DisplayName = "User Login Check")]
        public void CheckUserLogin()
        {
            bool IsLoginValidation(IEnumerable<EntityValidation> validations) =>
                validations.Count() == 1 && validations.ElementAt(0).Field == nameof(Login);

            var nullLogin = InvalidUsers[1];
            var emptyLogin = InvalidUsers[0];
            var spaceLogin = InvalidUsers[2];

            Assert.True(IsLoginValidation(nullLogin.IsValid()));
            Assert.True(IsLoginValidation(emptyLogin.IsValid()));
            Assert.True(IsLoginValidation(spaceLogin.IsValid()));
        }
    }
}
