using Estacionamento.Application.Commands.EntregaVeiculo;
using Estacionamento.Domain.DomainObjetcs.Messsages;
using Estacionamento.Domain.DTOs;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Entities.Veiculos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Commands.SaidaVeiculo
{
    public class SaidaVeiculoCommand : Command<Unit>
    {
        public string Placa { get; set; }

        public SaidaVeiculoCommand(string placa)
        {
            Placa = placa;
        }

        public override bool EhValido()
        {
            ValidationResult = new SaidaVeiculoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public List<string> messageErro()
        {
            ValidationResult = new SaidaVeiculoCommandValidation().Validate(this);
            return ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

        }

        public class SaidaVeiculoCommandValidation : AbstractValidator<SaidaVeiculoCommand>
        {
            public const string PLACA_VEICULO_INVALIDA = "Placa Veículo Inválida";
  

            public SaidaVeiculoCommandValidation()
            {

                RuleFor(c => c.Placa)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(PLACA_VEICULO_INVALIDA);



            }
        }
    }
}
