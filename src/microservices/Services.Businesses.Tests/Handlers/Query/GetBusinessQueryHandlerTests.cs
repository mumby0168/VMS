using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.Business.Domain;
using Services.Business.Handlers.Query;
using Services.Business.Messages.Queries;
using Services.Business.Repositorys;
using Services.Tests.Mocks;
using Shouldly;

namespace Services.Businesses.Tests.Handlers.Query
{
    public class GetBusinessQueryHandlerTests
    {
        private Mock<IBusinessRepository> _repository;
        private Guid _valid;
        private Guid _invalid;

        private Mock<IBusinessDocument> _business;
        private Mock<IHeadContact> _headContact;
        private Mock<IHeadOffice> _headOffice;



        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IBusinessRepository>();
            _business = new Mock<IBusinessDocument>();
            _valid = Guid.NewGuid();
            _invalid = Guid.NewGuid();
            _headContact = new Mock<IHeadContact>();
            _headOffice = new Mock<IHeadOffice>();
            _business.Setup(o => o.GetContact()).Returns(_headContact.Object);
            _business.Setup(o => o.GetOffice()).Returns(_headOffice.Object);

            _repository.Setup(o => o.GetBusinessAsync(_valid)).Returns(Task.FromResult(_business.Object));
        }

        [Test]
        public async Task HandleAsync_Always_ReturnsNullIfNoBusinessIsFound()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var dto = await sut.HandleAsync(new GetBusiness {Id = _invalid});

            //Assert
            dto.ShouldBeNull();
        }

        [Test]
        public async Task HandleAsync_Always_ReturnsDtoIfBusinessIsFound()  
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var dto = await sut.HandleAsync(new GetBusiness { Id = _valid });

            //Assert
            dto.ShouldNotBeNull();
        }


        public GetBusinessQueryHandler CreateSut() => new GetBusinessQueryHandler(LoggerMock.CreateVms<GetBusinessQueryHandler>(), _repository.Object);    
    }
}
