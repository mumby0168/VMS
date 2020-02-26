using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using Services.Common.Exceptions;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Repositorys;
using Services.Visitors.Services;

namespace Services.Visitors.Tests
{
    public class VisitorFormValidatorServiceTests
    {
        private AutoMocker _autoMocker;
        private Mock<IDataSpecificationRepository> _repo;
        private Mock<IDataSpecificationValidator> _validator;

        private Guid _validSpec = Guid.NewGuid();
        private Guid _invalidSpec = Guid.NewGuid();
        private Guid _validBusinessId = Guid.NewGuid();

        private List<VisitorDataEntry> _entries;    
        private List<IDataSpecificationDocument> _specs;
        
        
        [SetUp]
        public void SetUp()
        {
            _autoMocker = new AutoMocker();
            _repo = new Mock<IDataSpecificationRepository>();
            _validator = new Mock<IDataSpecificationValidator>();
            _autoMocker.Use(_validator);
            _autoMocker.Use(_repo);
            
            _entries = new List<VisitorDataEntry>();
            var validMock = new Mock<IDataSpecificationDocument>();
            validMock.SetupGet(o => o.Id).Returns(_validSpec);
            
            _specs = new List<IDataSpecificationDocument>();
            _specs.Add(validMock.Object);

            _repo.Setup(o => o.GetEntriesAsync(_validBusinessId)).Returns(Task.FromResult<IEnumerable<IDataSpecificationDocument>>(_specs));
        }

        [TestCase]
        public void Validate_Always_ThrowsIfNoSpecifications()
        {
            //Arrange
            var sut = CreateSut();

            _repo.Setup(o => o.GetEntriesAsync(It.IsAny<Guid>())).Returns(
                Task.FromResult<IEnumerable<IDataSpecificationDocument>>(new List<IDataSpecificationDocument>()));

            //Act
            //Assert
            Assert.ThrowsAsync<VmsException>(() => sut.Validate(Guid.Empty, _entries));
        }

        [TestCase]
        public void Validate_Always_ThrowsIfFieldDoesNotMatchSpec()
        {
            //Arrange
            var sut = CreateSut();
            var invalidEntry = new VisitorDataEntry(_invalidSpec, "");
            _entries.Add(invalidEntry);

            //Act
            //Assert
            Assert.ThrowsAsync<VmsException>(() => sut.Validate(_validBusinessId, _entries));
        }
        
        [TestCase]
        public void Validate_Always_ThrowsIfFieldValidationFails()
        {
            //Arrange
            var sut = CreateSut();
            var validEntry = new VisitorDataEntry(_validSpec, "");
            _entries.Add(validEntry);

            _validator.Setup(o => o.IsDataValid(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            
            //Act
            //Assert
            Assert.ThrowsAsync<VmsException>(() => sut.Validate(_validBusinessId, _entries));
        }

        [TestCase]
        public async Task Validate_Always_ReturnsIfDataValid()
        {
            //Arrange
            var sut = CreateSut();
            var validEntry = new VisitorDataEntry(_validSpec, "");
            _entries.Add(validEntry);

            _validator.Setup(o => o.IsDataValid(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            
            //Act
            //Assert
            await sut.Validate(_validBusinessId, _entries);
            Assert.True(true);
        }
        
        
        


        private IVisitorFormValidatorService CreateSut() => _autoMocker.CreateInstance<VisitorFormValidatorService>();


    }
}