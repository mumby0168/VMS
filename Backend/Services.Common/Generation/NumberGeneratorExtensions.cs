using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Common.Generation
{
    public static class NumberGeneratorExtensions
    {
        public static IServiceCollection AddNumberGenerator(this IServiceCollection services) =>
            services.AddTransient<INumberGenerator, NumberGenerator>();
    }
}
