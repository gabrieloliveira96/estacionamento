using Estacionamento.Domain.Data;
using Estacionamento.Domain.Entities.Veiculos;
using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Entities.Vagas
{
    public class Vaga : Entity
    {
       
        public string NumeroVaga { get; private set; }

        public TipoVagaEnum TipoVaga { get; private set; }

        public int Ocupada { get; private set; }

        public Vaga(int id,string numeroVaga,TipoVagaEnum tipoVaga,int ocupada = 0)
        {
            Id=id;
            NumeroVaga = numeroVaga.Trim();
            TipoVaga = tipoVaga;
            Ocupada = ocupada;
        }

        public void EditarVaga( string numeroVaga)
        {
            NumeroVaga = numeroVaga;
        }

        public void OcuparVaga()
        {
            Ocupada = 1;
        }
        public void DesocuparVaga()
        {
            Ocupada = 0;
        }
    }
}
