using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.Business.Factories;
using Services.Business.Handlers.Command;
using Services.Business.Messages.Commands;
using Services.Business.Messages.Events;
using Services.Business.Messages.Events.Rejected;
using Services.Business.Repositorys;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Shouldly.Configuration;

namespace Services.Businesses.Tests.Handlers
{
    public class CreateBusinessHandlerTests
    {

        private Mock<ILogger<CreateBusinessHandler>> _logger;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IBusinessRepository> _repo;
        private Mock<IRequestInfo> _request;
        private Mock<IBusinessesFactory> _factory;
        private Mock<Business.Domain.Business> _business;

        private const string TestCode = "TestCode";
        private const string TestReason = "TestReason";

        private Mock<CreateBusiness> _message;

        [SetUp]
        public void Setup()
        {
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _logger = new Mock<ILogger<CreateBusinessHandler>>();
            _repo = new Mock<IBusinessRepository>();
            _message = new Mock<CreateBusiness>();
            _request = new Mock<IRequestInfo>();
            _factory = new Mock<IBusinessesFactory>();
            _business = new Mock<Business.Domain.Business>();
            _factory.Setup(o => o.CreateBusiness(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_business.Object);
        }


        [Test]
        public async Task HandleAsync_Always_CreatesBusinessWhenDataValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _request.Object);

            //Assert
            _repo.Verify(o =>o.Add(_business.Object));
            _publisher.Verify(o => o.PublishEvent(It.IsAny<BusinessCreated>(), _request.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesRejectedMessageWhenDataInvalid()
        {
            //Arrange
            var sut = CreateSut();
            _factory.Setup(o => o.CreateBusiness(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Throws(new VmsException(TestCode, TestReason));

            //Act
            await sut.HandleAsync(_message.Object, _request.Object);

            //Assert
            VerifyFailure(TestCode, TestReason);
        }

        private void VerifyFailure(string code, string contains = null)
        {
            _publisher.Verify(o => o.PublishEvent(It.Is<CreateBusinessRejected>(m => m.Code == code && m.Reason.Contains(contains)), _request.Object));
        }

        //private void SetupContact(string firstName, string secondName, string contactNumber, string email)
        //{
        //    _message.SetupGet(o => o.HeadContactFirstName).Returns(firstName);
        //    _message.SetupGet(o => o.HeadContactSecondName).Returns(secondName);
        //    _message.SetupGet(o => o.HeadContactContactNumber).Returns(contactNumber);
        //    _message.SetupGet(o => o.HeadContactEmail).Returns(email);
        //}

        //private void SetupBusiness(string name, string trading, string website)
        //{
        //    _message.SetupGet(o => o.Name).Returns(name);
        //    _message.SetupGet(o => o.TradingName).Returns(trading);
        //    _message.SetupGet(o => o.WebAddress).Returns(website);
        //}

        //private void SetupOffice(string postcode, string line1, string line2)
        //{
        //    _message.SetupGet(o => o.HeadOfficeAddressLine1).Returns(line1);
        //    _message.SetupGet(o => o.HeadOfficeAddressLine2).Returns(line2);
        //    _message.SetupGet(o => o.HeadOfficePostCode).Returns(postcode);
        //}

        //private void SetupValidOffice() => SetupOffice("HU78LD", "Test line 1", "Test Line 2");

        //private void SetupValidContact() => SetupContact("Joe", "Bloggs", "07568512672", "joe.bloggs@outlook.com");

        //private void SetupValidBusiness() => SetupBusiness("Test", "Test", "http://www.google.co.uk");

        public CreateBusinessHandler CreateSut() => new CreateBusinessHandler(_repo.Object, _logger.Object, _publisher.Object, _factory.Object);
    }
}
