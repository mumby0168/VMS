using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Services.Common.Middleware
{
    public static class Extensions
    {
        public static IApplicationBuilder UseVmsExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<VmsExceptionMiddleware>();
    }
}
