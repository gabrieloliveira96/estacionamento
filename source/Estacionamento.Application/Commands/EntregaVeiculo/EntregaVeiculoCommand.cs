using Estacionamento.Domain.DomainObjetcs.Messsages;
using Estacionamento.Domain.DTOs.VeiculoDTO;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Entities.Veiculos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Commands.EntregaVeiculo
{
    public class EntregaVeiculoCommand : Command<Unit>
    {
        public VeiculoRequestDTO  VeiculoRequestDTO { get; set; }

        public EntregaVeiculoCommand(VeiculoRequestDTO veiculoRequestDTO)
        {
            VeiculoRequestDTO = veiculoRequestDTO;
        }

        public override bool EhValido()
        {
            ValidationResult = new EntregaVeiculoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public List<string> messageErro()
        {
            ValidationResult = new EntregaVeiculoCommandValidation().Validate(this);
            return ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

        }
        public RegistroVeiculo ToEntity(EntregaVeiculoCommand command, Vaga vaga)
        {
            var dadosVeiculo = command.VeiculoRequestDTO;
            return new RegistroVeiculo(dadosVeiculo.Marca, dadosVeiculo.Modelo,dadosVeiculo.Ano,dadosVeiculo.Placa,dadosVeiculo.Cor,dadosVeiculo.TipoVeiculo,vaga.Id);
        }
        public class EntregaVeiculoCommandValidation : AbstractValidator<EntregaVeiculoCommand>
        {
            public const string MARCA_VEICULO_INVALIDA = "Marca Veículo Inválida";
            public const string MODELO_VEICULO_INVALIDA = "Modelo Veículo Inválida";
            public const string COR_VEICULO_INVALIDA = "Cor Veículo Inválida";
            public const string ANO_VEICULO_INVALIDA = "Ano Veículo Inválida";
            public const string PLACA_VEICULO_INVALIDA = "Placa Veículo Inválida";

            public EntregaVeiculoCommandValidation()
            {

                RuleFor(c => c.VeiculoRequestDTO.Marca)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(MARCA_VEICULO_INVALIDA);


                RuleFor(c => c.VeiculoRequestDTO.Modelo)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(MODELO_VEICULO_INVALIDA);

                RuleFor(c => c.VeiculoRequestDTO.Cor)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(COR_VEICULO_INVALIDA);

                RuleFor(c => c.VeiculoRequestDTO.Ano)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(ANO_VEICULO_INVALIDA);


                RuleFor(c => c.VeiculoRequestDTO.Placa)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(PLACA_VEICULO_INVALIDA);



            }
        }
    }
}
