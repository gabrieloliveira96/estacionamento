using Estacionamento.Domain.Data;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Entities.Veiculos;
using Estacionamento.Domain.Interfaces.Repositories;
using Estacionamento.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Estacionamento.Infra.Repositories
{
    public class RepositoryVaga: IRepositoryVaga
    {
        private readonly EstacionamentoContext _dbContext;
        public RepositoryVaga(EstacionamentoContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public IUnitOfWork UnitOfWork => _dbContext;

        public Vaga Add(Vaga evento)
        {
            _dbContext.Set<Vaga>().Add(evento);
            return evento;
        }

        public void Delete(Vaga evento)
        {
            _dbContext.Set<Vaga>().Remove(evento);
        }

        public void DeleteRange(Vaga[] evento)
        {
            _dbContext.Set<Vaga>().RemoveRange(evento);
        }

        public IEnumerable<Vaga> Get(Expression<Func<Vaga, bool>> predicate)
        {
            return _dbContext.Set<Vaga>().Where(predicate).AsQueryable().ToList();
        }

        public Vaga Update(Vaga evento)
        {
            _dbContext.Set<Vaga>().Update(evento);
            return evento;
        }
    }
}
