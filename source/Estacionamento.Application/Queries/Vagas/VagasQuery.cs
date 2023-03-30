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

namespace Estacionamento.Application.Queries.Vagas
{
    public class VagasQuery : IVagasQuery
    {
        private readonly IConfiguration _config;
        private readonly ILogger<VagasQuery> _logger;
        private readonly IMediator _mediator;

        public VagasQuery(IConfiguration config, ILogger<VagasQuery> logger, IMediator mediator)
        {
            _config = config;
            _logger = logger;
            _mediator = mediator;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqliteConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<List<VagasDTO>> VagasGrande()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                    V.NumeroVaga AS NumeroVaga,
                                    V.TipoVaga AS TipoVaga,
                                    V.Ocupada AS Ocupada
                                  FROM T_VAGA V WHERE V.TipoVaga = '" + TipoVagaEnum.Grande + "' ";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<VagasDTO, VagasQuery>(query, _logger);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VagasDTO>> VagasMedia()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                    V.NumeroVaga AS NumeroVaga,
                                    V.TipoVaga AS TipoVaga,
                                    V.Ocupada AS Ocupada
                                  FROM T_VAGA V WHERE V.TipoVaga = '" + TipoVagaEnum.Media + "' ";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<VagasDTO, VagasQuery>(query, _logger);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VagasDTO>> VagasPequena()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                    V.NumeroVaga AS NumeroVaga,
                                    V.TipoVaga AS TipoVaga,
                                    V.Ocupada AS Ocupada
                                  FROM T_VAGA V WHERE V.TipoVaga = '" + TipoVagaEnum.Pequena + "' ";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<VagasDTO, VagasQuery>(query, _logger);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> TotalVagas()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT  COUNT(V.Id) AS Quantidade FROM T_VAGA V";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<QuantidadeVagasDTO, VagasQuery>(query, _logger);

                    return "Temos um total de " + result.FirstOrDefault().Quantidade + " vagas no estacionamento    ";
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<VagasDTO>> VagasDisponiveis()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                    V.NumeroVaga AS NumeroVaga,
                                    V.TipoVaga AS TipoVaga,
                                    V.Ocupada AS Ocupada
                                  FROM T_VAGA V WHERE V.Ocupada = 0";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<VagasDTO, VagasQuery>(query, _logger);

                    return result.ToList();



                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<VagasDTO>> VagasOcupadas()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = @"SELECT
                                    V.NumeroVaga AS NumeroVaga,
                                    V.TipoVaga AS TipoVaga,
                                    V.Ocupada AS Ocupada
                                  FROM T_VAGA V WHERE V.Ocupada = 1";

                    var query = new Dictionary<string, object>() { { sql, new { } } };

                    var result = connection.PesquisarSlapper<VagasDTO, VagasQuery>(query, _logger);

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
