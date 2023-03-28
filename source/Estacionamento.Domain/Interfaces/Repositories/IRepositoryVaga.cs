using Estacionamento.Domain.Data;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Entities.Veiculos;
using Estacionamento.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces.Repositories
{
    public interface IRepositoryVaga : IRepositoryBase<Vaga> { }

}
