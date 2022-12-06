using CasaAzul.Domain.Notification.Interface;
using FluentValidation.Results;

namespace CasaAzul.Domain.Notification
{
    public class DomainNotification : IDomainNotification
    {
        private List<NotificationMessage> _notifications;
        public DomainNotification()
        {
            _notifications = new List<NotificationMessage>();
        }

        public bool HasNotifications => Notifications.Any();

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach(var errors in validationResult.Errors)
            {
                _notifications.Add(new NotificationMessage(errors.ErrorCode, errors.ErrorMessage));
            }
        }
    }
}
