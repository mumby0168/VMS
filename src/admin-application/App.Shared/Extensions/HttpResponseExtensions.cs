using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App.Shared.Extensions
{
    public static class HttpResponseExtensions
    {
        public static Guid GetOperationId(this HttpResponseMessage message)
        {
            if (message.StatusCode == HttpStatusCode.Accepted)
            {
                return message.Headers.Contains("X-Operation") ? Guid.Parse(message.Headers.GetValues("X-Operation").FirstOrDefault()) : Guid.Empty;
            }
            else return Guid.Empty;
        }
    }
}
