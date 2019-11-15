using System.ComponentModel.DataAnnotations;
namespace App.Businesses.Models
{
    public class HeadOffice
    {
        [DataType(DataType.PostalCode)]
        [Required]
        public string PostCode { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
    }
}