using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Common.Logging;

namespace Services.Tests.Mocks
{
    public static class LoggerMock
    {
        public static ILogger<T> Create<T>() where T : class => new Mock<ILogger<T>>().Object;

        public static IVmsLogger<T> CreateVms<T>() where T : class => new Mock<IVmsLogger<T>>().Object;
    }
}
