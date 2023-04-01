
using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Estacionamento.Domain.Interfaces.Queries;
using Estacionamento.Domain.DTOs.VagaDTO;

namespace Estacionamento.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VagaController : ControllerBase
    {
        private readonly IMediator _mediatorHandler;
        private readonly IVagasQuery _vagaQuery;

        public VagaController(IMediator mediatorHandler,
                               INotificationHandler<DomainNotification> notifications,
                               INotificationHandler<DomainSuccesNotification> succesNotifications,
                               IHttpContextAccessor httpContextAccessor,
                               IVagasQuery vagasQuery) : base(notifications, succesNotifications, mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
            _vagaQuery = vagasQuery;
        }



        [HttpGet("VagasDisponiveis")]
        public async Task<IActionResult> VagasDisponiveis()
        {
            return Ok(await _vagaQuery.VagasDisponiveis());
        }

        [HttpGet("VagasOcupadas")]
        public async Task<IActionResult> VagasOcupadas()
        {
            return Ok(await _vagaQuery.VagasOcupadas());
        }
        [HttpGet("TotalVagas")]
        public async Task<IActionResult> TotalVagas()
        {
            return Ok(await _vagaQuery.TotalVagas());
        }

        [HttpGet("Pequena")]
        public async Task<IActionResult> Pequena()
        {
            return Ok(await _vagaQuery.VagasPequena());
        }

        [HttpGet("Media")]
        public async Task<IActionResult> Media()
        {
            return Ok(await _vagaQuery.VagasMedia());
        }

        [HttpGet("Grande")]
        public async Task<IActionResult> Grande()
        {
            return Ok(await _vagaQuery.VagasGrande());
        }
        [HttpGet("Pesquisa")]
        public async Task<IActionResult> Pesquisa([FromQuery] FiltroVagasRequestDTO filtro)
        {
            return Ok(await _vagaQuery.FiltroVagas(filtro));
        }
    }
}