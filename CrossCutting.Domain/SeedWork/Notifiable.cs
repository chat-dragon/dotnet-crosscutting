using FluentValidation;
using FluentValidation.Results;

namespace CrossCutting.Domain.SeedWork;

public class Notifiable
{
    public record Notification
    {
        private readonly ValidationResult _result;
        private readonly string _publicMessase;
        private readonly string _internalMessage;

        public Notification(string publicMessase, string internalMessage, ValidationResult result)
        {
            _publicMessase = publicMessase;
            _internalMessage = internalMessage;
            _result = result;
        }

        public string PublicMessase => _publicMessase;
        public string InternalMessage => _internalMessage;
        public bool IsValid => _result?.IsValid ?? false;
        public ValidationResult FluentValidationResult => _result;
    }

    private readonly List<Notification> _notifications = new();

    public bool IsValid
    {
        get
        {
            bool valid = true;
            foreach (Notification n in _notifications)
            {
                valid &= n.IsValid;
            }
            return valid;
        }
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    protected void AddNotifications<T>(T value, string publicMessage, string internalMessage, AbstractValidator<T> validator)
    {
        var result = validator.Validate(value);
        SetResult(new Notification(!result.IsValid ? publicMessage : string.Empty, !result.IsValid ? internalMessage : string.Empty, result));
    }

    public string GetNotificationInternalMessage()
    {
        string message = string.Empty;
        foreach (Notification n in _notifications)
        {
            message += n.InternalMessage + Environment.NewLine;
        }
        return message.TrimEnd(Environment.NewLine.ToArray());
    }

    protected void ClearNotifications()
    {
        _notifications.Clear();
    }

    protected void SetResult(Notification notification)
    {
        _notifications.Add(notification);
    }

    protected void SetFailure(string name, string publicMessage, string? internalMessage = null)
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure(name, internalMessage)
        };
        var result = new ValidationResult(failures);
        SetResult(new Notification(publicMessage, internalMessage, result));
    }
}
