using System;

namespace Services.Visitors.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}