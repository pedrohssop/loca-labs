using LocaLabs.Api.Models;
using LocaLabs.Application.Services;
using System;
using System.Linq;
using System.Text.Json;

namespace LocaLabs.Api.Services
{
    public interface IOutputBuilderService
    {
        string CreateOutput(object data = default);
        string CreateOutput(Exception ex);
    }

    public class OutputBuilderService : IOutputBuilderService
    {
        private static Type OutputType =
            typeof(Output);

        INotificationService Notification { get; }
        public OutputBuilderService(INotificationService notification) =>
            Notification = notification;

        public string CreateOutput(object data = default)
        {
            var notifications = Notification.GetNotifications();
            var output = new Output
            {
                Data = data,
                Notifications = notifications,
                HasNotifications = notifications.Any(),
                IsRequestError = Notification.HasErrors,
            };

            return JsonSerializer.Serialize(output, OutputType);
        }

        public string CreateOutput(Exception ex)
        {
            var notifications = Notification.GetNotifications();
            var output = new Output
            {
                Error = ex,
                IsRequestError = false,
                Notifications = notifications,
                HasNotifications = notifications.Any(),
            };

            return JsonSerializer.Serialize(output, OutputType);
        }
    }
}
