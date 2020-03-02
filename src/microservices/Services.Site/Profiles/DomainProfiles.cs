using System.Security.Policy;
using AutoMapper;
using Services.Sites.Domain;
using Services.Sites.Dtos;

namespace Services.Sites.Profiles
{
    public class DomainProfiles : Profile
    {
        public DomainProfiles()
        {
            CreateMap<SiteDocument, SiteDto>();
        }
    }
}