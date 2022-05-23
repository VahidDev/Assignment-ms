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
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            CreateMap<RoleOffer,GetNestedRoleOfferDto>().ReverseMap();
            CreateMap<RoleOffer,RoleOfferDto>().ReverseMap();

            CreateMap<RoleOffer, NestedRoleOfferDto>().ReverseMap();
            CreateMap<NestedRoleOfferDto, RoleOfferDto>().ReverseMap();

            CreateMap<FunctionalAreaType,FunctionalAreaTypeDto>().ReverseMap();
            CreateMap<Volunteer, AssignOrWaitlistVolunteerDto>().ReverseMap();

            CreateMap<Filter,CreateFilterDto > ().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());

            CreateMap<GetFilterDto,Filter>().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToArrayConverter>()); 

            CreateMap<FunctionalRequirement, GetFunctionalRequirementDto>().ReverseMap();

            CreateMap<GetRequirementDto, Requirement>().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToArrayConverter>());

            CreateMap<Filter, UpdateFilterDto > ().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());

            CreateMap<UpdateTemplateDto, Template>().ReverseMap();
            CreateMap<Template, GetTemplateDto>().ReverseMap();
            CreateMap<Template, CreateTemplateDto>().ReverseMap();
            CreateMap<FunctionalArea, FunctionalAreaDto>().ReverseMap();
            CreateMap<Volunteer, VolunteerChangeToAnyStatusDto>().ReverseMap();
        }
    }
}
