using System;

namespace Api.Gateway.Dtos.Sites
{
    public class SiteUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Initials { get; set; }
    }
}
