using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Visitors.Commands;

namespace Services.Visitors.Handlers.Command
{
    public class CreateVisitorHandler : ICommandHandler<CreateVisitor>
    {
        public CreateVisitorHandler()
        {
            
        }
        public Task HandleAsync(CreateVisitor message, IRequestInfo requestInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}