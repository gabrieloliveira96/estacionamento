using Estacionamento.Domain.Interfaces.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estacionamento.Application.Queries.Base;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using System.Threading;
using Estacionamento.Domain.Entities.Vagas;
using Microsoft.Data.Sqlite;
using Estacionamento.Domain.DTOs.VagaDTO;
using Estacionamento.Domain.Enum;
using Estacionamento.Domain.DTOs.RegistroVeiculoDTO;

namespace Estacionamento.Application.Queries.RegistroVeiculos
{
    public class RegistroVeiculosQuery : IRegistroVeiculosQuery
    {
        private readonly IConfiguration _config;
        private readonly ILogger<RegistroVeiculosQuery> _logger;
        private readonly IMediator _mediator;

        public RegistroVeiculosQuery(IConfiguration config, ILogger<RegistroVeiculosQuery> logger, IMediator mediator)
        {
            _config = config;
            _logger = logger;
            _mediator = mediator;
        }

        public SqliteConnection Connection
        {
            get
            {
                return new SqliteConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<List<RegistrosGeraisDTO>> RegistrosGerais()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                   TR.Marca AS MARCA,
                                   TR.Modelo AS Modelo,
                                   TR.Cor AS Cor,
                                   TR.Ano AS Ano,
                                   TR.Placa AS Placa,
                                   TR.TipoVeiculo AS TipoVeiculo,
                                   VAGA.NumeroVaga AS NumeroVaga,
                                   TR.DataEntrada AS DataEntrada,
                                   TR.DataSaida AS DataSaida
                                  FROM T_REGISTRO_VEICULO TR
                                  LEFT JOIN T_VAGA VAGA ON VAGA.id = TR.VagaId";


                    var query = new Dictionary<string, object>() { { sql, new {  } } };

                    var result = connection.PesquisarSlapper<RegistrosGeraisDTO, RegistroVeiculosQuery>(query, _logger);

                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VeiculosVagaDTO>> VeiculosEstacionados()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                   VEICULO.Marca AS MARCA,
                                   VEICULO.Modelo AS Modelo,
                                   VEICULO.Cor AS Cor,
                                   VEICULO.Ano AS Ano,
                                   VEICULO.Placa AS Placa,
                                   VEICULO.TipoVeiculo AS TipoVeiculo,
                                   VAGA.NumeroVaga AS NumeroVaga,
                                   VEICULO.DataEntrada AS DataEntrada
                                  FROM T_REGISTRO_VEICULO VEICULO
                                  LEFT JOIN T_VAGA VAGA ON VAGA.id = VEICULO.VagaId
                                WHERE VEICULO.DataSaida is null ";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<VeiculosVagaDTO, RegistroVeiculosQuery>(query, _logger);

                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
