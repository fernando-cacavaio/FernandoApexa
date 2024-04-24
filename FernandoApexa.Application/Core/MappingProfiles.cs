using AutoMapper;
using FernandoApexa.Application.Advisors;
using FernandoApexa.Domain;

namespace FernandoApexa.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Advisor, Advisor>();
            CreateMap<Advisor, AdvisorDto>();
        }
    }
}