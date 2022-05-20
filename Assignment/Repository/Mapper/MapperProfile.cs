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
            CreateMap<CreateFilterDto ,Filter> ().ReverseMap()
                .ForMember(r => r.Value, r => r.Ignore());
            CreateMap<GetFilterDto,Filter>().ReverseMap()
                .ForMember(r=>r.Value,r=>r.Ignore());
            CreateMap<FunctionalRequirement, GetFunctionalRequirementDto>().ReverseMap();
            CreateMap<GetRequirementDto, Requirement>().ReverseMap()
                .ForMember(r=>r.Value,r=>r.Ignore());
            CreateMap< UpdateFilterDto,Filter > ().ReverseMap()
                .ForMember(r => r.Value, r => r.Ignore()); 
            CreateMap<UpdateTemplateDto, Template>().ReverseMap()
                .ForMember(r=>r.Filters,r=>r.Ignore()); 
            CreateMap<Template, GetTemplateDto>().ReverseMap()
                .ForMember(r => r.Filters, r => r.Ignore()); 
            CreateMap<Template, CreateTemplateDto>().ReverseMap()
                .ForMember(r => r.Filters, r => r.Ignore()); 
            CreateMap<FunctionalArea, FunctionalAreaDto>().ReverseMap();
            CreateMap<Volunteer, VolunteerChangeToAnyStatusDto>().ReverseMap();
        }
    }
}
