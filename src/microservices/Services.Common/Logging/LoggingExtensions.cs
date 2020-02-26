using System;
using System.Collections.Generic;
using System.Net;
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
            // var addresses = Dns.GetHostAddresses("logs");
            // opts.Address = addresses[0].ToString();

            var addresses = Dns.GetHostAddresses(Dns.GetHostName());
            opts.Address = addresses[0].ToString();

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
