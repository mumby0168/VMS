namespace Services.Business.Dtos
{
    public class BusinessDto
    {
        public BusinessDto()
        {
            Office = new HeadOfficeDto();
            Contact = new HeadContactDto();
        }
    
        public string Name { get; set; }
       
        public string TradingName { get; set; }
        

        public string WebAddress { get; set; }
        
        public HeadOfficeDto Office { get; set; }

        public HeadContactDto Contact { get; set; }
    }
}