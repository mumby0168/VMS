using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App.Shared.Exceptions
{
    public class InternalHttpRequestException : Exception
    {
        public InternalHttpRequestException(HttpRequestException inner) : base("A request failed to be made", inner)
        {            
        }
    }
}
