using System;

namespace Services.Sites.Dtos
{
    public class FireListUserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }
    }
}
