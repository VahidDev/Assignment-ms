using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;

namespace Repository.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            CreateMap<RoleOffer,GetRoleOffersDto>().ReverseMap();
            CreateMap<RoleOffer,RoleOfferDto>().ReverseMap();
            CreateMap<RoleOfferDto,NestedRoleOfferDto>().ReverseMap();
            CreateMap<FunctionalAreaType,FunctionalAreaTypeDto>().ReverseMap();
            CreateMap<Volunteer, AssignOrWaitlistVolunteerDto>().ReverseMap();
            CreateMap<Filter, CreateFilterDto>().ReverseMap();
            CreateMap<Filter, GetFilterDto>().ReverseMap();
            CreateMap<Filter, UpdateFilterDto>().ReverseMap(); 
            CreateMap<Template, UpdateTemplateDto>().ReverseMap(); 
            CreateMap<Template, GetTemplateDto>().ReverseMap(); 
            CreateMap<Template, CreateTemplateDto>().ReverseMap(); 
            CreateMap<FunctionalArea, FunctionalAreaDto>().ReverseMap();
            CreateMap<Volunteer, VolunteerChangeToAnyStatusDto>().ReverseMap();
        }
    }
}
