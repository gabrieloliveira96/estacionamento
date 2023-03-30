using Estacionamento.Domain.DTOs.VagaDTO;
using Estacionamento.Domain.DTOs.VeiculoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces.Queries
{
    public interface IVeiculosQuery
    {
        Task<List<VeiculosVagaDTO>> VeiculosEstacionados();

    }
}
