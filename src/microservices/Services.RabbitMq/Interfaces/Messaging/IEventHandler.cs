using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{   
    public interface IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T message, IRequestInfo requestInfo);
    }
}
