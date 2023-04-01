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
using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;

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

        StringBuilder QueryBuilderWhere(FiltroRegistrosRequestDTO obj)
        {
            StringBuilder resultBuilder = new(" WHERE ");
            string[] query = CheckParams(obj);

            if (!query.Any())
            {
                return new StringBuilder("");
            }

            resultBuilder.Append(string.Join(" AND ", query));
            return resultBuilder;
        }
        string[] CheckParams(FiltroRegistrosRequestDTO obj)
        {
            IList<string> paramlist = new List<string>();

            if (obj.Marca != null)
                paramlist.Add("TR.Marca LIKE '%" + obj.Marca + "%'");

            if (obj.Placa != null)
                paramlist.Add("TR.Placa LIKE '%" + obj.Placa + "%'");

            if (obj.Cor != null)
                paramlist.Add("TR.Cor LIKE '%" + obj.Cor + "%'");

            if (obj.Modelo != null)
                paramlist.Add("TR.Modelo LIKE '%" + obj.Modelo + "%'");

            if (obj.Ano != null)
                paramlist.Add("TR.Ano LIKE '%" + obj.Ano + "%'");

            if (obj.NumeroVaga != null)
                paramlist.Add("VAGA.NumeroVaga LIKE '%" + obj.NumeroVaga + "%'");

            if (obj.TipoVeiculo != null)
            {
                switch (obj.TipoVeiculo)
                {
                    case TipoVeiculoEnum.Carro:
                        paramlist.Add("TR.TipoVeiculo = '" + TipoVeiculoEnum.Carro + "'");
                        break;
                    case TipoVeiculoEnum.Moto:
                        paramlist.Add("TR.TipoVeiculo = '" + TipoVeiculoEnum.Moto+ "'");
                        break;
                    case TipoVeiculoEnum.Van:
                        paramlist.Add("TR.TipoVeiculo = '" + TipoVeiculoEnum.Van + "'");
                        break;
                    default:
                        break;
                }
            }
            
            if (obj.DataEntrada != null && obj.DataSaida != null)
                paramlist.Add(" strftime('%d/%m/%Y',TR.DataEntrada) BETWEEN '" + obj.DataEntrada + "' AND '" + obj.DataSaida + "'");


            return paramlist.ToArray();
        }
        public async Task<List<FiltroRegistrosResponseDTO>> FiltroRegistrosVeiculos(FiltroRegistrosRequestDTO filtro)
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
                                  LEFT JOIN T_VAGA VAGA ON VAGA.id = TR.VagaId" + QueryBuilderWhere(filtro);


                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<FiltroRegistrosResponseDTO, RegistroVeiculosQuery>(query, _logger);

                    return result.ToList();

                }
            }
            catch
            {
                throw;
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
