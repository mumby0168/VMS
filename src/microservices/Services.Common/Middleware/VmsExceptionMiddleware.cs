using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.IO;
using Services.Common.Exceptions;
using Services.Common.Models;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Services.Common.Middleware
{
    public class VmsExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (VmsException e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(VmsError.Create(e.Code, e.Message)));
            }
        }
    }
}
