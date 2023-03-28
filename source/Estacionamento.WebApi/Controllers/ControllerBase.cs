using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Estacionamento.WebApi.Controllers
{
    // [EnableCors("InspecaoCorsPolicy")]
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly DomainSuccesNotificationHandler _succesNotifications;
        private readonly IMediator _mediatorHandler;

        protected Guid ClienteId;

        protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                                 INotificationHandler<DomainSuccesNotification> succesNotifications,
                                 IMediator mediatorHandler,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _succesNotifications = (DomainSuccesNotificationHandler)succesNotifications;
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }
        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }
        protected IEnumerable<string> ObterMensagensDeSucesso()
        {
            return _succesNotifications.ObterNotificacoes().Select(c => c.Value).ToList();

        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.Publish(new DomainNotification(codigo, mensagem));
        }


        protected new IActionResult Response()
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = ObterMensagensDeSucesso()
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = ObterMensagensErro()
            });
        }
    }
}
