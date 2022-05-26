using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Repository.Converters;
using System;

namespace Repository.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateTemplateDto, Template>().ReverseMap();
            CreateMap<Template, GetTemplateDto>().ReverseMap();
            CreateMap<Template, CreateTemplateDto>().ReverseMap();
            
            CreateMap<Volunteer, VolunteerChangeToAnyStatusDto>().ReverseMap();
            CreateMap<Volunteer, AssignOrWaitlistVolunteerDto>().ReverseMap();
            
            CreateMap<FunctionalArea, FunctionalAreaDto>().ReverseMap();

            CreateMap<Location, LocationDto>().ReverseMap();

            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            
            CreateMap<RoleOffer,GetNestedRoleOfferDto>().ReverseMap(); // not used (should consider to remove)
            CreateMap<RoleOffer,RoleOfferDto>().ReverseMap();
            CreateMap<RoleOffer, NestedRoleOfferDto>().ReverseMap();
            CreateMap<NestedRoleOfferDto, RoleOfferDto>().ReverseMap();

            CreateMap<FunctionalAreaType,FunctionalAreaTypeDto>().ReverseMap();

            CreateMap<Filter,CreateFilterDto > ().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());
            CreateMap<GetFilterDto,Filter>().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToArrayConverter>());
            CreateMap<Filter, UpdateFilterDto>().ReverseMap()
               .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());

            CreateMap<UpdateRequirementDto, UpdateRequirementConvertibleDto>().ReverseMap()
               .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());
            CreateMap<UpdateRequirementDto, Requirement>().ReverseMap();
            CreateMap<GetRequirementDto, Requirement>().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToArrayConverter>());

            CreateMap<FunctionalRequirement, GetFunctionalRequirementDto>().ReverseMap();
            CreateMap<UpdateFunctionalRequirementDto, UpdateFunctionalRequirementConvertibleDto>().ReverseMap();
        }
    }
}
