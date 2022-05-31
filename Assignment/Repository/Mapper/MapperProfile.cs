using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Repository.Converters;

namespace Repository.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateTemplateDto, Template>().ReverseMap();
            CreateMap<Template, GetTemplateDto>().ReverseMap();
            CreateMap<Template, CreateTemplateDto>().ReverseMap();
            
            CreateMap<Volunteer, VolunteerChangeToAnyStatusDto>().ReverseMap()
                .ForMember(r=>r.CandidateId, r=>r.MapFrom(r=>r.Id));
            CreateMap<Volunteer, AssignOrWaitlistVolunteerDto>().ReverseMap()
                .ForMember(r => r.CandidateId, r => r.MapFrom(r => r.Id));
            
            CreateMap<FunctionalArea, FunctionalAreaDto>().ReverseMap();

            CreateMap<Location, LocationDto>().ReverseMap();

            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            
            CreateMap<RoleOffer,RoleOfferDto>().ReverseMap();
            CreateMap<RoleOffer, NestedRoleOfferDto>().ReverseMap();
            CreateMap<NestedRoleOfferDto, RoleOfferDto>().ReverseMap();

            CreateMap<FunctionalAreaType,FunctionalAreaTypeDto>().ReverseMap();

            CreateMap<Filter,CreateFilterDto>().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());
            CreateMap<GetFilterDto,Filter>().ReverseMap()
                .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToArrayConverter>());
            CreateMap<Filter, UpdateFilterDto>().ReverseMap()
               .ForMember(r => r.Value, r => r.MapFrom<RequirementValueToStringConverter>());

            CreateMap<Report, CreateReportDto>().ReverseMap()
                .ForMember(r => r.VolunteerColumns, r => r.MapFrom<VolunteerColumnsToStringConverter>())
                .ForMember(r => r.RoleOfferColumns, r => r.MapFrom<RoleOfferColumnsToStringConverter>());
            CreateMap<Report, UpdateReportDto>().ReverseMap()
                .ForMember(r => r.VolunteerColumns, r => r.MapFrom<VolunteerColumnsToStringConverter>())
                .ForMember(r => r.RoleOfferColumns, r => r.MapFrom<RoleOfferColumnsToStringConverter>());
            CreateMap<GetReportDto, Report>().ReverseMap()
                .ForMember(r=>r.RoleOfferFilters, r=>r.MapFrom(r=>r.RoleOfferTemplate.Filters))
                .ForMember(r => r.VolunteerFilters, r => r.MapFrom(r => r.VolunteerTemplate.Filters))
                .ForMember(r => r.VolunteerColumns, r => r.MapFrom<VolunteerColumnsToArrayConverter>())
                .ForMember(r => r.RoleOfferColumns, r => r.MapFrom<RoleOfferColumnsToArrayConverter>());

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
