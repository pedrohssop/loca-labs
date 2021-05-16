using LocaLabs.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocaLabs.Application.Services
{
    public interface INotificationService
    {
        bool HasErrors { get; }
        IEnumerable<string> GetNotifications();
        
        void AddError(Exception ex);
        void AddNotification(params string[] messages);
        void AddNotification(params EntityValidation[] validations);
    }

    internal class NotificationService : INotificationService
    {
        public bool HasErrors { get; set; }
        private HashSet<string> Messages { get; set; }

        public void AddError(Exception ex)
        {
            HasErrors = true;
            AddNotification($"[ERROR] - {ex.Message}");
        }

        public void AddNotification(params string[] messages)
        {
            if (messages.Length == 0)
                return;

            if (Messages is null)
                Messages = new HashSet<string>();

            foreach (var item in messages)
            {
                if (!string.IsNullOrEmpty(item))
                    Messages.Add(item);
            }
        }

        public void AddNotification(params EntityValidation[] validations)
        {
            if (validations.Length > 0)
                AddNotification(validations.Where(w => w is not null).Select(s => s.ToString()).ToArray());
        }

        public IEnumerable<string> GetNotifications()
        {
            if (Messages is null)
                return Enumerable.Empty<string>();

            return Messages;
        }
    }
}