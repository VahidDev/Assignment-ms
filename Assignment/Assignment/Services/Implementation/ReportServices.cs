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

            if (await _unitOfWork.ReportRepository.AnyAsync(r => r.Name == dto.Name))
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    "The report name has already been used");
            }
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
            if (await _unitOfWork.ReportRepository
                .AnyAsync(r =>r.Id != dto.Id && r.Name == dto.Name))
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    "The report name has already been used");
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
