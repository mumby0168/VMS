using System;

namespace Services.Sites.Dtos
{
    public class SiteDto
    {
        public Guid Id { get; set; }

        public Guid BusinessId { get; set; }

        public string Name { get; set; }

        public string PostCode { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }
    }

}
