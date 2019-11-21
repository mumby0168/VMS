using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App.Shared.Services
{
    public interface IHttpClient
    {
        HttpClient GatewayClient { get; }

        HttpClient IdentityClient { get; }
    }
}
