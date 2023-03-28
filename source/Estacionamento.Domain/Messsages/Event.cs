using MediatR;

namespace Estacionamento.Domain.DomainObjetcs.Messsages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
