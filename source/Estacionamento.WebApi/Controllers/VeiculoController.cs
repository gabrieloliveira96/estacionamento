
using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Estacionamento.Application.Commands.EntregaVeiculo;
using Estacionamento.Application.Commands.SaidaVeiculo;
using Estacionamento.Domain.DTOs.VeiculoDTO;
using Estacionamento.Domain.Interfaces.Queries;

namespace Estacionamento.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IMediator _mediatorHandler;
        private readonly IVeiculosQuery _veiculosQuery;


        public VeiculoController(IMediator mediatorHandler,
                               INotificationHandler<DomainNotification> notifications,
                               INotificationHandler<DomainSuccesNotification> succesNotifications,
                               IHttpContextAccessor httpContextAccessor,
                               IVeiculosQuery veiculosQuery) : base(notifications, succesNotifications, mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
            _veiculosQuery = veiculosQuery;
        }



        [HttpPost("Entrada")]
        public async Task<IActionResult> Entrada([FromBody] VeiculoRequestDTO veiculoEntrada)
        {
            var command = new EntregaVeiculoCommand(veiculoEntrada);

            await _mediatorHandler.Send(command);

            return Response();
        }

        [HttpPut("Saida")]
        public async Task<IActionResult> Saida(string placa)
        {
            var command = new SaidaVeiculoCommand(placa);

            await _mediatorHandler.Send(command);

            return Response();
        }

        [HttpGet("VeiculosEstacionados")]
        public async Task<IActionResult> VeiculosEstacionados()
        {
            return Ok(await _veiculosQuery.VeiculosEstacionados());
        }

    }
}