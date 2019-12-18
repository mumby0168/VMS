using System;

namespace Services.Users.Services.Dtos
{
    public class BusinessDto
    {
        public BusinessDto()
        {
        }

        public Guid Id { get; set; }
    
        public string Name { get; set; }
       
        public string TradingName { get; set; }
        

        public string WebAddress { get; set; }
        
    }
}