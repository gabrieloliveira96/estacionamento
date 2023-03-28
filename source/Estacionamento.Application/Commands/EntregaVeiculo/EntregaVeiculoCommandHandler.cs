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
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Commands.EntregaVeiculo
{
    public class EntregaVeiculoCommandHandler : IRequestHandler<EntregaVeiculoCommand, Unit>
    {
        private CancellationToken _cancellationToken;
        private readonly IMediator _mediator;
        private readonly IRepositoryRegistroVeiculo _repositoryRegistroVeiculo;
        private readonly IRepositoryVaga _repositoryVaga;

        public EntregaVeiculoCommandHandler(IMediator mediator, IRepositoryRegistroVeiculo repositoryRegistroVeiculo,IRepositoryVaga repositoryVaga)
        {
            _mediator = mediator;
            _repositoryRegistroVeiculo = repositoryRegistroVeiculo;
            _repositoryVaga = repositoryVaga;

        }
        public async Task<Unit> Handle(EntregaVeiculoCommand command, CancellationToken cancellationToken)
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
            var veiculoEstacionado = ObterVeiculo(command.VeiculoRequestDTO.Placa);
            if(veiculoEstacionado is not null)
            {
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Esse veículo já se encontra estacionado!"));
                return Unit.Value;
            }

            switch (command.VeiculoRequestDTO.TipoVeiculo)
            {
                case TipoVeiculoEnum.Carro:
                    EntradaCarro(command);
                    break;
                case TipoVeiculoEnum.Moto:
                    EntradaMoto(command);
                    break;
                case TipoVeiculoEnum.Van:
                    EntradaVan(command);
                    break;
                default:
                    await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Não é permitida a entrada desse tipo de veículo!"));
                    break;
            }


            return Unit.Value;

        }

 
        private async void EntradaCarro(EntregaVeiculoCommand command)
        {
            var vagasDisponiveis = ValidaVagaCarro();
            if(vagasDisponiveis is null)
            {
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Não há vaga disponivel para carro!"));
                return;
            }
          
            var vagaUtilizar = vagasDisponiveis.FirstOrDefault();

            vagaUtilizar.OcuparVaga();

            var veiculo = command.ToEntity(command, vagaUtilizar);

            _repositoryRegistroVeiculo.Add(veiculo);

            _repositoryVaga.Update(vagaUtilizar);

            if (_repositoryRegistroVeiculo.UnitOfWork.Commit().Result)
                await _mediator.Publish(new DomainSuccesNotification(key: "EntradaVeiculo", value: "Veículo estacionado com sucesso!"), _cancellationToken);
            else
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Erro ao acesso veículo!"), _cancellationToken);
           
        }
        private async void EntradaMoto(EntregaVeiculoCommand command)
        {
            var vagasDisponiveis = ValidaVagaMoto();
            if (vagasDisponiveis is null)
            {
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Não há vaga disponivel para moto!"));
                return;
            }

            var vagaUtilizar = vagasDisponiveis.FirstOrDefault();

            vagaUtilizar.OcuparVaga();

            var veiculo = command.ToEntity(command, vagaUtilizar);

            _repositoryRegistroVeiculo.Add(veiculo);

            _repositoryVaga.Update(vagaUtilizar);

            if (_repositoryRegistroVeiculo.UnitOfWork.Commit().Result)
                await _mediator.Publish(new DomainSuccesNotification(key: "EntradaVeiculo", value: "Veículo estacionado com sucesso!"), _cancellationToken);
            else
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Erro ao acesso veículo!"), _cancellationToken);

        }
        private async void EntradaVan(EntregaVeiculoCommand command)
        {
            var vagasDisponiveis = ValidaVagaVan();
            if (vagasDisponiveis is null)
            {
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Não há vaga disponivel para van!"));
                return;
            }

            var vagaGrande = vagasDisponiveis.Where(v=>v.TipoVaga == TipoVagaEnum.Grande).Select(v=>v).FirstOrDefault();

            if(vagaGrande is not null)
            {

                vagaGrande.OcuparVaga();

                var veiculo = command.ToEntity(command, vagaGrande);
                
                _repositoryRegistroVeiculo.Add(veiculo);
                
                _repositoryVaga.Update(vagaGrande);
            }
            else
            {
                var vagaMedia = vagasDisponiveis.Where(v => v.TipoVaga == TipoVagaEnum.Media).Select(v => v).ToList();

                if(vagaMedia.Count < 3)
                {
                    await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Não há vaga disponive!"));
                    return;
                }

                var vagaSequencial = ValidaSequenciaVaga(vagaMedia);

                if(vagaSequencial.Count == 0)
                {
                    await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Não há vaga disponive!"));
                    return;
                }

                if (vagaSequencial.Count >= 3)
                {
                    vagaSequencial.ForEach(vaga =>
                    {
                        vaga.OcuparVaga();

                        var veiculo = command.ToEntity(command, vaga);

                        _repositoryRegistroVeiculo.Add(veiculo);

                        _repositoryVaga.Update(vaga);
                    });
                }

            }


            if (_repositoryRegistroVeiculo.UnitOfWork.Commit().Result)
                await _mediator.Publish(new DomainSuccesNotification(key: "EntradaVeiculo", value: "Veículo estacionado com sucesso!"), _cancellationToken);
            else
                await _mediator.Publish(new DomainNotification(key: "EntradaVeiculo", value: "Erro ao acesso veículo!"), _cancellationToken);
        }

        private List<Vaga> ValidaSequenciaVaga(List<Vaga> vagas)
        {
            var idVagaRef = vagas.FirstOrDefault().Id;
            var index = 0;
            var vagasUtilizar = new List<Vaga>();
            foreach (var vaga in vagas)
            {
                if (index == 3)
                    return vagasUtilizar;

                if(idVagaRef == vaga.Id)
                {
                    vagasUtilizar.Add(vaga);
                    index++;
                    continue;
                }

                if (idVagaRef + 1 == vaga.Id)
                {
                    vagasUtilizar.Add(vaga);
                    idVagaRef++;
                    index++;
                }
                else
                {
                    vagasUtilizar = new List<Vaga>();
                }
                    
            }
            return vagasUtilizar;

        }

        private List<Vaga> ValidaVagaCarro()
        {
            var vagaDisponivel = _repositoryVaga.Get(v => v.Ocupada == 0 && (v.TipoVaga == TipoVagaEnum.Media || v.TipoVaga == TipoVagaEnum.Grande)).Where(v => v.Ocupada == 0 && (v.TipoVaga == TipoVagaEnum.Media || v.TipoVaga == TipoVagaEnum.Grande)).ToList();
            
            if (!vagaDisponivel.Any())
                return null;
            
            return vagaDisponivel;
        }

        private List<Vaga> ValidaVagaMoto()
        {
            var vagaDisponivel = _repositoryVaga.Get(v => v.Ocupada == 0).Where(v => v.Ocupada == 0).ToList();

            if (!vagaDisponivel.Any())
                return null;

            return vagaDisponivel;
        }

        private List<Vaga> ValidaVagaVan()
        {
            var vagaDisponivel = _repositoryVaga.Get(v => v.Ocupada == 0 && (v.TipoVaga == TipoVagaEnum.Media || v.TipoVaga == TipoVagaEnum.Grande)).Where(v => v.Ocupada == 0 && (v.TipoVaga == TipoVagaEnum.Media || v.TipoVaga == TipoVagaEnum.Grande)).ToList();

            if (!vagaDisponivel.Any())
                return null;

            return vagaDisponivel;
        }

        private RegistroVeiculo ObterVeiculo(string placa)
        {
            var veiculo = _repositoryRegistroVeiculo.Get(v => v.Placa == placa && v.DataSaida == null).Where(v => v.Placa == placa && v.DataSaida == null).FirstOrDefault();

            if (veiculo is null)
                return null;

            return veiculo;
        }


    }
}