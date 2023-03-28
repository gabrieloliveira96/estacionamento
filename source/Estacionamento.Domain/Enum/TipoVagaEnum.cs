using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Enum
{
    public enum TipoVagaEnum
    {
        [Description("Pequena")]
        Pequena = 0,
        [Description("Media")]
        Media = 1,
        [Description("Grande")]
        Grande = 2
    }
}
