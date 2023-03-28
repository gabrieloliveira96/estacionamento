using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Messsages.CommonMessages.Notifications
{
    public class DomainSuccesNotificationHandler : INotificationHandler<DomainSuccesNotification>
    {
        private List<DomainSuccesNotification> _notifications;

        public DomainSuccesNotificationHandler()
        {
            _notifications = new List<DomainSuccesNotification>();
        }

        public Task Handle(DomainSuccesNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<DomainSuccesNotification> ObterNotificacoes()
        {
            return _notifications;
        }

        public virtual bool TemNotificacao()
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainSuccesNotification>();
        }
    }
}
