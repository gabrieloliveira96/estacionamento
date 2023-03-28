using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Enum
{
    public enum TipoVeiculoEnum
    {
        [Description("Carro")]
        Carro = 0,
        [Description("Moto")]
        Moto = 1,
        [Description("Van")]
        Van = 2
    }
}
