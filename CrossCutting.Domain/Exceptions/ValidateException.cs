using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CrossCutting.Domain.SeedWork.Notifiable;

namespace CrossCutting.Domain.Exceptions
{
    public class ValidateException : Exception
    {
        public ValidateException(IReadOnlyCollection<Notification> notifications)
        {
            Notifications = notifications;
        }

        public IReadOnlyCollection<Notification> Notifications { get; }
    }
}
