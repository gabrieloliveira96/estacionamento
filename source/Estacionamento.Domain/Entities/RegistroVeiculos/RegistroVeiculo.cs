using Estacionamento.Domain.Data;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Entities.Veiculos
{
    public class RegistroVeiculo : Entity, IAggregateRoot
    {
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Ano { get; private set; }
        public string Cor { get; private  set; }
        public string Placa { get; private set; }

        public TipoVeiculoEnum TipoVeiculo { get; private set; }
        public virtual Vaga? Vaga { get; private set; }
        public virtual int VagaId { get; private set; }
        public DateTime? DataEntrada { get; private set; }
        public DateTime? DataSaida { get; private set; }

        public RegistroVeiculo(string marca, string modelo, string ano, string placa, string cor, TipoVeiculoEnum tipoVeiculo, int vagaId)
        {
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Placa = placa;
            Cor = cor;
            TipoVeiculo = tipoVeiculo;
            VagaId = vagaId;
            DataEntrada = DateTime.Now;
            DataSaida = null;
        }

        public void RegistraSaidaVeiculo()
        {
            DataSaida = DateTime.Now;
        }
    }
}
