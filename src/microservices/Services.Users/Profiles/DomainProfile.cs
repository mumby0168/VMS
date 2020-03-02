using AutoMapper;
using Services.Users.Controllers;
using Services.Users.Domain;

namespace Services.Users.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<UserDocument, UserDto>();
        }
    }
}