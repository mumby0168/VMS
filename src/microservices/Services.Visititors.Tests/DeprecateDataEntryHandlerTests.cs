using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Events;
using Services.Visitors.Handlers.Command;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Tests
{
    public class DeprecateDataEntryHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IDataSpecificationRepository> _repository;
        private Guid _targetId = Guid.NewGuid();
        private Mock<IDataSpecificationDocument> _target;

        private IEnumerable<IDataSpecificationDocument> GetEntries(int count, int target)
        {
            for (int i = 0; i < count; i++)
            {
                var specMock = new Mock<IDataSpecificationDocument>();
                if (i == target)
                {
                    _target.Setup(o => o.Order).Returns(i + 1);
                    yield return _target.Object;
                }
                specMock.SetupGet(o => o.Order).Returns(i + 1);
                yield return specMock.Object;
            }
            
        }


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<IDataSpecificationRepository>();
            _target = new Mock<IDataSpecificationDocument>();
            _target.Setup(o => o.Id).Returns(_targetId);
        }

        [Test]
        public async Task HandleAsync_Always_PublishesDeprecationRejectedWhenNotDataForBusiness()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(new DeprecateDataEntry(Guid.NewGuid(), Guid.NewGuid()), _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<DataEntryDeprecationRejected>(x => x.Code == Codes.InvalidBusinessId), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesDeprecationRejectedWhenDataSpecNotFound()
        {
            //Arrange
            var sut = CreateSut();

            _repository.Setup(o => o.GetEntriesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(GetEntries(10, -1)));

            //Act
            await sut.HandleAsync(new DeprecateDataEntry(Guid.NewGuid(), Guid.NewGuid()), _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<DataEntryDeprecationRejected>(x => x.Code == Codes.InvalidId), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_DeprecatesAndUpdatesTargetSpec()
        {
            //Arrange
            var sut = CreateSut();

            _repository.Setup(o => o.GetEntriesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(GetEntries(10, 4)));

            //Act
            await sut.HandleAsync(new DeprecateDataEntry(_targetId, Guid.NewGuid()), _requestInfo.Object);

            //Assert
            _target.Verify(o => o.Deprecate());
            _repository.Verify(o => o.UpdateAsync(_target.Object));
        }

        [TestCase(20, 5)]
        [TestCase(2, 1)]
        [TestCase(27, 8)]
        public async Task HandleAsync_Always_ReOrderRemainingSpecs(int collectionSize, int targetLocation)    
        {
            //Arrange
            var sut = CreateSut();

            var entries = GetEntries(collectionSize, targetLocation);

            _repository.Setup(o => o.GetEntriesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(entries));


            //Act
            await sut.HandleAsync(new DeprecateDataEntry(_targetId, Guid.NewGuid()), _requestInfo.Object);

            //Assert
            for (int i = 0; i < collectionSize - 1; i++)
            {
                _repository.Verify(o => o.UpdateAsync(It.IsAny<IDataSpecificationDocument>()));
            }
        }

        [TestCase(20, 5)]
        [TestCase(2, 1)]
        [TestCase(27, 8)]
        public async Task HandleAsync_Always_PublhisesDeprecatedWhenComplete(int collectionSize, int targetLocation)
        {
            //Arrange   
            var sut = CreateSut();

            var entries = GetEntries(collectionSize, targetLocation);

            _repository.Setup(o => o.GetEntriesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(entries));


            //Act
            await sut.HandleAsync(new DeprecateDataEntry(_targetId, Guid.NewGuid()), _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<DataEntryDeprecated>(), _requestInfo.Object));
        }

        public DeprecateDataEntryHandler CreateSut() => new DeprecateDataEntryHandler(LoggerMock.CreateVms<DeprecateDataEntryHandler>(), _publisher.Object, _repository.Object);  
    }
}
