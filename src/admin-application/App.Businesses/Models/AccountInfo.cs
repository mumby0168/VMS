using System;
using System.Collections.Generic;
using System.Text;

namespace App.Businesses.Models
{
    public class AccountInfo
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPending { get; set; }
    }
}
