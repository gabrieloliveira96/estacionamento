using FluentValidation.Results;
using MediatR;

namespace Estacionamento.Domain.DomainObjetcs.Messsages
{
    public abstract class Command<T> : Message, IRequest<T>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            ValidationResult = new ValidationResult();
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }

}
