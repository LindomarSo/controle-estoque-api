using FluentValidation.Results;

namespace CasaAzul.Domain.Notification.Interface
{
    public interface IDomainNotification
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        void AddNotifications(ValidationResult validationResult);
    }
}
