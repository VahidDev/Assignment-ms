using Assignment.Constants.ReportConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using Assignment.Utilities.ServicesUtilities.MapperUtilities;
using Assignment.Utilities.ServicesUtilities.ReportUtilities;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    internal class ReportServices : IReportServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJsonFactory _jsonFactory;

        public ReportServices(IUnitOfWork unitOfWork, IMapper mapper, IJsonFactory jsonFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jsonFactory = jsonFactory;
        }

        public async Task<ObjectResult> CreateReportAsync(CreateReportDto dto)
        {
            Report report = _mapper.Map<Report>(dto);

            if (dto.VolunteerFilters != null)
            {
                report.VolunteerTemplate = ReportTemplatesCreator
                    .CreateVolunteerTemplateAndMapFilters
                    (_mapper.Map<ICollection<Filter>>(dto.VolunteerFilters));
            }
            if (dto.RoleOfferFilters != null)
            {
                report.RoleOfferTemplate = ReportTemplatesCreator
                    .CreateRoleOfferTemplateAndMapFilters
                    (_mapper.Map<ICollection<Filter>>(dto.RoleOfferFilters));
            }

            await _unitOfWork.ReportRepository.AddAsync(report);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<ObjectResult> DeleteByIdAsync(int id)
        {
            Report dbReport = await _unitOfWork.ReportRepository
                .GetByIdIncludingItemsAsNoTrackingAsync(id);
            if (dbReport == null)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound
                    , "Report is not found");
            }
            _unitOfWork.ReportRepository.Delete(dbReport);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<ObjectResult> GetAllOptionsAsync()
        {
            GetAllOptionsDto dtoToSend = new();

            ICollection<FunctionalAreaTypeDto> functionalAreaTypeDtos 
                = _mapper.Map<ICollection<FunctionalAreaTypeDto>>
                (await _unitOfWork.FunctionalAreaTypeRepository
                .GetAllAsNoTrackingAsync(r=>!r.IsDeleted));

            ICollection<FunctionalAreaDto> functionalAreas
                = _mapper.Map<ICollection<FunctionalAreaDto>>
                (await _unitOfWork.FunctionalAreaRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted));

            ICollection<JobTitleDto> jobTitles
                = _mapper.Map<ICollection<JobTitleDto>>
                (await _unitOfWork.JobTitleRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted));

            ICollection<LocationDto> locations
                = _mapper.Map<ICollection<LocationDto>>
                (await _unitOfWork.LocationRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted));

            dtoToSend.EntityOptions.Name 
                = ReportFilterNameConstants.FunctionalAreaType;
            dtoToSend.FunctionalAreaOptions.Name 
                = ReportFilterNameConstants.FunctionalArea;
            dtoToSend.JobTitleOptions.Name 
                = ReportFilterNameConstants.JobTitle;
            dtoToSend.LocaionOptions.Name
                = ReportFilterNameConstants.Location;

            foreach (FunctionalAreaTypeDto fat in functionalAreaTypeDtos)
            {
                dtoToSend.EntityOptions.ValueOptions.Add(fat.Name);
            }

            foreach (FunctionalAreaDto fa in functionalAreas)
            {
                dtoToSend.FunctionalAreaOptions.ValueOptions.Add(fa.Name);
            }

            foreach (JobTitleDto jobTitle in jobTitles)
            {
                dtoToSend.JobTitleOptions.ValueOptions
                    .Add(jobTitle.Name);
            }

            foreach (LocationDto location in locations)
            {
                dtoToSend.LocaionOptions.ValueOptions
                    .Add(location.Name);
            }


            return _jsonFactory.CreateJson(StatusCodes.Status200OK, null,dtoToSend);
        }

        public async Task<ObjectResult> GetAllReportsAsync()
        {
            return _jsonFactory.CreateJson
                (
                StatusCodes.Status200OK,
                null,
                _mapper.Map<List<GetReportDto>>(
                await _unitOfWork.ReportRepository
                .GetAllAsNoTrackingIncludingItemsAsync(r => !r.IsDeleted)
                ));
        }

        public async Task<ObjectResult> UpdateReportAsync(UpdateReportDto dto)
        {
            Report dbReport = await _unitOfWork.ReportRepository
                .GetByIdIncludingItemsAsNoTrackingAsync(dto.Id ?? 0);
            if (dbReport == null)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound
                    ,"Report is not found");
            }
            
            Report updatedReport = UpdateReportMapper
                .MapToReport(dto,dbReport,_mapper.Map<Report>(dto), _mapper);

            if (dbReport.RoleOfferTemplate != null && dbReport.RoleOfferTemplate.Filters != null)
            {
                if(updatedReport.RoleOfferTemplate.Filters == null)
                {
                    updatedReport.RoleOfferTemplate.Filters = new List<Filter>();
                }
                foreach (Filter filter in dbReport.RoleOfferTemplate.Filters)
                {
                    if (!updatedReport.RoleOfferTemplate.Filters.Any(f => f.Id == filter.Id))
                    {
                        filter.IsDeleted = true;
                        updatedReport.RoleOfferTemplate.Filters.Add(filter);
                    }
                }
            }

            if (dbReport.VolunteerTemplate != null && dbReport.VolunteerTemplate.Filters != null)
            {
                if (updatedReport.VolunteerTemplate.Filters == null)
                {
                    updatedReport.VolunteerTemplate.Filters = new List<Filter>();
                }
                foreach (Filter filter in dbReport.VolunteerTemplate.Filters)
                {
                    if (!updatedReport.VolunteerTemplate.Filters.Any(f => f.Id == filter.Id))
                    {
                        filter.IsDeleted = true;
                        updatedReport.VolunteerTemplate.Filters.Add(filter);
                    }
                }
            }
            _unitOfWork.ReportRepository.Update(updatedReport);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
