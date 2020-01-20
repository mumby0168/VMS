using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Common.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddUdpLogging(this IServiceCollection services)
        {
            services.AddSingleton<ServiceLoggingOptions>();
            services.AddTransient<ILogEncoder, LogEncoder>();
            services.AddSingleton<IUdpLoggingClient, UdpLoggingClient>();
            services.AddSingleton(typeof(IVmsLogger<>), typeof(VmsLogger<>));
            return services;
        }

        public static IApplicationBuilder UseUdpLogging(this IApplicationBuilder application, string serviceName)
        {
            var opts = application.ApplicationServices.GetService<ServiceLoggingOptions>();

            opts.ServiceName = serviceName;
            opts.Port = 11000;
            opts.Address = "172.22.0.4";

            var client = application.ApplicationServices.GetService<IUdpLoggingClient>();
            client.CreateConnection();

            return application;
        }

        public static byte[] GetBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static byte[] GetBytes(this ushort number)
        {
            return BitConverter.GetBytes(number);
        }

        public static ushort ToUShort(this int value)
        {
            return (ushort) value;
        }
    }
}
