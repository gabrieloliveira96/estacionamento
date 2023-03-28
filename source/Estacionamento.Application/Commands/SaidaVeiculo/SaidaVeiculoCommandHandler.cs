using Estacionamento.Application.Commands.EntregaVeiculo;
using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Entities.Veiculos;
using Estacionamento.Domain.Enum;
using Estacionamento.Domain.Interfaces.Repositories;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Commands.SaidaVeiculo
{
    public class SaidaVeiculoCommandHandler : IRequestHandler<SaidaVeiculoCommand, Unit>
    {
        private CancellationToken _cancellationToken;
        private readonly IMediator _mediator;
        private readonly IRepositoryRegistroVeiculo _repositoryRegistroVeiculo;
        private readonly IRepositoryVaga _repositoryVaga;

        public SaidaVeiculoCommandHandler(IMediator mediator, IRepositoryRegistroVeiculo repositoryRegistroVeiculo, IRepositoryVaga repositoryVaga)
        {
            _mediator = mediator;
            _repositoryRegistroVeiculo = repositoryRegistroVeiculo;
            _repositoryVaga = repositoryVaga;

        }
        public async Task<Unit> Handle(SaidaVeiculoCommand command, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            if (!command.EhValido())
            {
                foreach (var item in command.messageErro())
                {
                    await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: item), cancellationToken);
                }

                return Unit.Value;
            }

            var veiculo = ObterVeiculo(command.Placa);

            if(veiculo is null)
            {
                await _mediator.Publish(new DomainNotification(key: "SaidaVeiculo", value: "Veículo não encontrado!"));
                return Unit.Value;
            }

            SaidaVeiculo(veiculo);

            return Unit.Value;

        }

        private async void SaidaVeiculo(List<RegistroVeiculo> veiculos)
        {
            veiculos.ForEach(v =>
            {

                var vaga = ObterVaga(v.VagaId);

                v.RegistraSaidaVeiculo();

                vaga.DesocuparVaga();

                _repositoryRegistroVeiculo.Update(v);

                _repositoryVaga.Update(vaga);
            });

            if (_repositoryRegistroVeiculo.UnitOfWork.Commit().Result)
                await _mediator.Publish(new DomainSuccesNotification(key: "SaidaVeiculo", value: "Saída do Veículo registrada com sucesso!"), _cancellationToken);
            else
                await _mediator.Publish(new DomainNotification(key: "SaidaVeiculo", value: "Erro ao tentar sair com o veículo!"), _cancellationToken);

        }
        private Vaga ObterVaga(int idVaga)
        {
            return _repositoryVaga.Get(v => v.Id == idVaga).Where(v => v.Id == idVaga).FirstOrDefault();
        }

        private List<RegistroVeiculo> ObterVeiculo(string placa)
        {
            var veiculo = _repositoryRegistroVeiculo.Get(v=>v.Placa == placa && v.DataSaida == null).Where(v=>v.Placa == placa && v.DataSaida == null).ToList();

            if (veiculo is null)
                return null;

            return veiculo;
        }


    }
}