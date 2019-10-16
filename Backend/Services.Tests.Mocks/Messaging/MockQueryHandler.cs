using System.Threading.Tasks;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Tests.Mocks.Messaging
{
    public class MockQueryHandler : IQueryHandler<MockQuery, string>
    {
        public virtual Task<string> HandleAsync(MockQuery query) => Task.FromResult("test");
    }
}
