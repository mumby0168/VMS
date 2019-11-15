using App.Shared.Exceptions;
using App.Shared.Operations;
using App.Shared.Operations.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Tests.Operations
{
    public class OperationsManagerTests
    {

        private Mock<IOperationsService> _service;
        private Mock<ILogger<OperationsManager>> _logger;
        private Mock<IOperationsClient> _client;
        private Mock<IOperationMessage> _message;
        private readonly Guid _containedId = Guid.NewGuid();
        private readonly Guid _notContainedId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IOperationsService>();
            _client = new Mock<IOperationsClient>();
            _logger = new Mock<ILogger<OperationsManager>>();
            _message = new Mock<IOperationMessage>();
            _message.SetupAllProperties();
        }

        [Test]
        public void MessageRecievedHandler_Always_AddsToMessages()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            _service.Raise(s => s.MessageReceived += null,EventArgs.Empty, new Mock<IOperationMessage>().Object);

            //Assert
            sut.Messages.Count().ShouldBe(1);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ConnectionStatusUpdatedHandler_Always_UpdatesConnectionStatus(bool isConnected)
        {
            //Arrange
            var sut = CreateSut();
            var agrs = new ConnectionStatusUpdatedEventArgs(isConnected);

            //Act
            _service.Raise(s => s.ConnectionStatusUpdated += null, EventArgs.Empty, agrs);

            //Assert
            sut.IsConnected.ShouldBe(isConnected);
        }


        [Test]
        public async Task GetOperationStatus_Always_FallsbackToClient()
        {
            //Arrange
            var sut = CreateSut();            

            _client.Setup(o => o.GetOperationMessageAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_message.Object));

            //Act
            var result = await sut.GetOperationStatus(_notContainedId);

            //Assert
            _client.Verify(o => o.GetOperationMessageAsync(It.IsAny<Guid>()));
            result.ShouldBe(_message.Object);
        }

        [Test]
        public async Task GetOperationStatus_Always_UsesMessageQueueFirst()
        {
            //Arrange
            var sut = CreateSut();
            _message.SetupGet(o => o.Id).Returns(_containedId);
            _service.Raise(s => s.MessageReceived += null, EventArgs.Empty, _message.Object);

            //Act
            var result = await sut.GetOperationStatus(_containedId);

            //Assert
            result.ShouldBe(_message.Object);
            sut.Messages.ShouldBeEmpty();
        }

        [Test]
        public async Task GetOperationStatus_Always_ThrowsIfMessageCannotBeRetreived()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            //Assert
            await Should.ThrowAsync<OperationNotFoundException>(sut.GetOperationStatus(_notContainedId));
        }

        private IOperationsManager CreateSut()
        {
            return new OperationsManager(_service.Object, _logger.Object, _client.Object);
        }
    }
}
