using System.ComponentModel.DataAnnotations;
namespace App.Businesses.Models
{
    public class Business
    {
        public Business()
        {
            Office = new HeadOffice();
            Contact = new HeadContact();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TradingName { get; set; }

        [Url]
        public string WebAddress { get; set; }

        public HeadOffice Office { get; set; }

        public HeadContact Contact { get; set; }
    }
}