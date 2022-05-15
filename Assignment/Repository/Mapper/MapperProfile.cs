using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;

namespace Repository.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<RoleOffer,RoleOfferDto>().ReverseMap();
            CreateMap<Volunteer, VolunteerDto>().ReverseMap();
            CreateMap<Filter, CreateFilterDto>().ReverseMap();
            CreateMap<Filter, UpdateFilterDto>().ReverseMap(); 
            CreateMap<Template, UpdateTemplateDto>().ReverseMap(); 
            CreateMap<Template, CreateTemplateDto>().ReverseMap(); 
            CreateMap<FunctionalArea, FunctionalAreaDto>().ReverseMap();
            CreateMap<Volunteer, VolunteerChangeToAnyStatusDto>().ReverseMap();
        }
    }
}
