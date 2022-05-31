using Assignment.Factory;
using Assignment.Services.Abstraction;
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
            report.VolunteerTemplate = new Template 
            { 
                Name = nameof(Report.VolunteerTemplate) ,
                Filters = _mapper.Map<ICollection<Filter>>(dto.VolunteerFilters)
            };
            report.RoleOfferTemplate = new Template
            {
                Name = nameof(Report.RoleOfferTemplate),
                Filters = _mapper.Map<ICollection<Filter>>(dto.RoleOfferFilters)
            };
            await _unitOfWork.ReportRepository.AddAsync(report);
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
    }
}
