using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace App.Businesses.Models
{
    public class HeadContact
    {
        [Required]
         public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}