using LocaLabs.Application.Services;
using Moq;
using System.Collections.Generic;

namespace LocaLabs.Tests.Application
{
    public class HandlerCaseBase
    {
        protected List<string> Notifications { get; }
        public HandlerCaseBase()
        {
            Notifications = new List<string>();
        }

        protected INotificationService NotificationMoq()
        {
            var moq = new Mock<INotificationService>();
            moq.Setup(s => s.AddNotification(It.IsAny<string[]>()))
               .Callback<string[]>(s => Notifications.AddRange(s));

            return moq.Object;
        }
    }
}