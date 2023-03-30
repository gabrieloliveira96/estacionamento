using Estacionamento.Domain.DTOs.VagaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces.Queries
{
    public interface IVagasQuery
    {
        Task<List<VagasDTO>> VagasDisponiveis();
        Task<List<VagasDTO>> VagasOcupadas();
        Task<string> TotalVagas();
        Task<List<VagasDTO>> VagasPequena();
        Task<List<VagasDTO>> VagasMedia();
        Task<List<VagasDTO>> VagasGrande();

    }
}
