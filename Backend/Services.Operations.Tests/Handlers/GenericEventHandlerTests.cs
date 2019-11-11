using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.Operations.Handlers;
using Services.Operations.Services;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks.Messaging;

namespace Services.Operations.Tests.Handlers
{
    public class GenericEventHandlerTests
    {

        private Mock<ILogger<GenericBusHandler>> _logger;
        private Mock<IOperationsCache> _cache;

        private MockRejectedEvent _rejectedEvent;
        private Mock<IEvent> _event;
        private Mock<IRequestInfo> _requestInfo;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<GenericBusHandler>>();
            _cache = new Mock<IOperationsCache>();
            _rejectedEvent = new MockRejectedEvent();
            _event = new Mock<IEvent>();
            
            _requestInfo = new Mock<IRequestInfo>();
        }

        [Test]
        public async Task HandleAsync_Always_SavesRejectedEvent()
        {
            //Arrange
            var sut = CreateSut();
            _requestInfo.SetupGet(o => o.State).Returns(RequestState.Failed);

            //Act
            await sut.HandleAsync(_rejectedEvent, _requestInfo.Object);

            //Assert
            _requestInfo.Verify(o => o.Fail());
            _cache.Verify(o => o.SaveAsync(_requestInfo.Object.OperationId, RequestState.Failed.ToString().ToLower(),_rejectedEvent.Code, _rejectedEvent.Reason));
        }

        [Test]
        public async Task HandleAsync_Always_SavesHappyEvent()
        {
            //Arrange
            var sut = CreateSut();
            _requestInfo.SetupGet(o => o.State).Returns(RequestState.Complete);

            //Act
            await sut.HandleAsync(_event, _requestInfo.Object);

            //Assert
            _requestInfo.Verify(o => o.Complete());
            _cache.Verify(o => o.SaveAsync(_requestInfo.Object.OperationId, RequestState.Complete.ToString().ToLower(), null, null));
        }



        public GenericBusHandler CreateSut() => new GenericBusHandler(_logger.Object, _cache.Object);

    }
}
