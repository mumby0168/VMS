using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace App.Businesses.Models
{
    public class HeadContact
    {
         public string FirstName { get; set; }
        
        public string SecondName { get; set; }
               
        public string ContactNumber { get; set; }
        
        public string Email { get; set; }
    }
}