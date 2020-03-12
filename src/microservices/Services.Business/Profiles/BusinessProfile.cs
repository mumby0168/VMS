using AutoMapper;
using Services.Business.Dtos;

namespace Services.Business.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Domain.BusinessDocument, BusinessSummaryDto>();
        }
    }
}