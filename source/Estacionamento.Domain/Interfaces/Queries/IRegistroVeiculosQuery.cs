using Estacionamento.Domain.DTOs.RegistroVeiculoDTO;
using Estacionamento.Domain.DTOs.VagaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces.Queries
{
    public interface IRegistroVeiculosQuery
    {
        Task<List<VeiculosVagaDTO>> VeiculosEstacionados();

        Task<List<RegistrosGeraisDTO>> RegistrosGerais();

        Task<List<FiltroRegistrosResponseDTO>> FiltroRegistrosVeiculos(FiltroRegistrosRequestDTO filtro);


    }
}
