using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;

namespace Services.Tests.Mocks
{
    public static class LoggerMock
    {
        public static ILogger<T> Create<T>() where T : class => new Mock<ILogger<T>>().Object;
    }
}
