using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace System_Admin.Areas.Account.Models
{
    public interface IJwtToken
    {
         string RawToken { get; }

         string Role { get; }   

         IEnumerable<Claim> Claims { get; }     

         DateTime Expiry { get; } 
    }
}