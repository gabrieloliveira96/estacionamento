using Estacionamento.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        T Add(T evento);
        T Update(T evento);
        void Delete(T evento);
        void DeleteRange(T[] evento);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IUnitOfWork UnitOfWork { get; }
    }
}
