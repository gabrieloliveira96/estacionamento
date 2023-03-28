using Estacionamento.Domain.Data;
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
    public class RepositoryRegistroVeiculo: IRepositoryRegistroVeiculo
    {
        private readonly EstacionamentoContext _dbContext;
        public RepositoryRegistroVeiculo(EstacionamentoContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public IUnitOfWork UnitOfWork => _dbContext;

        public RegistroVeiculo Add(RegistroVeiculo evento)
        {
            _dbContext.Set<RegistroVeiculo>().Add(evento);
            return evento;
        }

        public void Delete(RegistroVeiculo evento)
        {
            _dbContext.Set<RegistroVeiculo>().Remove(evento);
        }

        public void DeleteRange(RegistroVeiculo[] evento)
        {
            _dbContext.Set<RegistroVeiculo>().RemoveRange(evento);
        }

        public IEnumerable<RegistroVeiculo> Get(Expression<Func<RegistroVeiculo, bool>> predicate)
        {
            return _dbContext.Set<RegistroVeiculo>().Where(predicate).AsQueryable().ToList();
        }

        public RegistroVeiculo Update(RegistroVeiculo evento)
        {
            _dbContext.Set<RegistroVeiculo>().Update(evento);
            return evento;
        }
    }
}
