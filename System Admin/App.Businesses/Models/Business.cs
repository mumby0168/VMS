using System;
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
        
        public Guid Id { get; set; }

        public string Name { get; set; }
       
        public string TradingName { get; set; }
        

        public string WebAddress { get; set; }
        
        public HeadOffice Office { get; set; }

        public HeadContact Contact { get; set; }
    }
}