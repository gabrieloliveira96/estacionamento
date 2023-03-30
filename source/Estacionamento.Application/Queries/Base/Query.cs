using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Queries.Base
{
    public static class Query
    {

        public static IEnumerable<T> PesquisarSlapper<T, TL>(this IDbConnection connect, Dictionary<string, object> query, ILogger<TL> logger) where TL : class
        {
            IEnumerable<T> resultado = null;
            try
            {
                foreach (KeyValuePair<string, object> t in query)
                {
                    var stopWatch = new Stopwatch();

                    stopWatch.Start();

                    logger.LogInformation("Abrindo Conexão com Banco", connect);

                    connect.Open();

                    stopWatch.Stop();

                    var tempoAberturaConexao = stopWatch.Elapsed;

                    logger.LogInformation("Conexão aberta em {tempoAberturaConexao}(ms)", tempoAberturaConexao);

                    stopWatch.Restart();

                    logger.LogInformation("Iniciando consulta: {query}", query);

                    IEnumerable<dynamic> dados = connect.Query<dynamic>(t.Key, t.Value);

                    stopWatch.Stop();

                    var tempoConsulta = stopWatch.ElapsedMilliseconds;

                    var dadosObtidos = JsonConvert.SerializeObject(dados);

                    logger.LogInformation("Consulta realizada em {tempoConsulta}(ms); Dados obtidos: {dadosObtidos}", tempoConsulta, dadosObtidos);

                    resultado = (Slapper.AutoMapper.MapDynamic<T>(dados) as IEnumerable<T>);

                    Slapper.AutoMapper.Cache.ClearAllCaches();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}