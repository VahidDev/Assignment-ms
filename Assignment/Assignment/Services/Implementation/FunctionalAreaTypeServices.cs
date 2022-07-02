using Assignment.Constants.VolunteerConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    internal class FunctionalAreaTypeServices : IFunctionalAreaTypeServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonFactory _jsonFactory;

        public FunctionalAreaTypeServices(IMapper mapper, IUnitOfWork unitOfWork, 
            IJsonFactory jsonFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jsonFactory = jsonFactory;
        }

        public async Task<ObjectResult> GetAllIncludingItemsAsync()
        {
            IReadOnlyCollection<RoleOffer> dbRoleOffers 
                = (await _unitOfWork.RoleOfferRepository
                .GetAllIncludingItemsAsync())
                .ToList();

            IReadOnlyCollection<Volunteer> volunteers 
                = (await _unitOfWork.VolunteerRepository
                .GetAllAsNoTrackingAsync(r => r.RoleOfferId != null && !r.IsDeleted))
                .ToList();

            List<FunctionalAreaType> distinctFunctionalAreaTypes 
                = dbRoleOffers
                .Select(r => r.FunctionalAreaType)
                .DistinctBy(r => r.Name)
                .ToList();
            List<FunctionalArea> distinctFunctionalAreas = dbRoleOffers
               .Select(r => r.FunctionalArea)
               .DistinctBy(r => r.Code)
               .ToList();
            List<Location> distinctLocations = dbRoleOffers
                .Select(r => r.Location)
                .DistinctBy(r => r.Code)
                .ToList();
            List<JobTitle> distinctJobTitles = dbRoleOffers
                .Select(r => r.JobTitle)
                .DistinctBy(r => r.Code)
                .ToList();

            foreach (FunctionalAreaType functionalAreaType in distinctFunctionalAreaTypes)
            {
                ICollection<FunctionalArea> functionalAreas = dbRoleOffers
                    .Where(r => r.FunctionalAreaType.Name == functionalAreaType.Name)
                    .DistinctBy(r => r.FunctionalArea.Name)
                    .Select(r => r.FunctionalArea)
                    .ToList();
                functionalAreaType.FunctionalAreas = functionalAreas;
            }

            foreach (FunctionalArea functionalArea in distinctFunctionalAreas)
            {
                ICollection<JobTitle> jobTitles = dbRoleOffers
                .Where(r => r.FunctionalArea.Code == functionalArea.Code)
                .DistinctBy(r => r.JobTitle.Code)
                .Select(r => r.JobTitle)
                .ToList();
                functionalArea.JobTitles = jobTitles;
            }

            foreach (JobTitle jobTitle in distinctJobTitles)
            {
                ICollection<Location> locations = dbRoleOffers
                    .Where(r => r.JobTitle.Code == jobTitle.Code)
                    .DistinctBy(r => r.Location.Code)
                    .Select(r => r.Location)
                    .ToList();
                jobTitle.Locations = locations;
            }

            List<FunctionalAreaTypeDto> functionalAreaTypes = _mapper
                .Map<List<FunctionalAreaTypeDto>>(distinctFunctionalAreaTypes);

            if (functionalAreaTypes.Count == 0)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status200OK, null, functionalAreaTypes);
            }

            foreach (RoleOffer roleOffer in dbRoleOffers)
            {
                FunctionalAreaTypeDto functionalAreaType = functionalAreaTypes
                    .First(r => r.Id == roleOffer.FunctionalAreaType.Id);
                FunctionalAreaDto functionalArea = functionalAreaType.FunctionalAreas
                     .First(r => r.Id == roleOffer.FunctionalArea.Id);
                JobTitleDto jobTitle = functionalArea.JobTitles
                    .First(j => j.Id == roleOffer.JobTitle.Id);
                LocationDto location = jobTitle.Locations
                    .First(l => l.Id == roleOffer.Location.Id);

                location.RoleOffer = _mapper.Map<NestedRoleOfferDto>(roleOffer);

                if (location.RoleOffer.FunctionalRequirement == null)
                {
                    location.RoleOffer.FunctionalRequirement 
                        = new GetFunctionalRequirementDto();
                    location.RoleOffer.FunctionalRequirement.Requirements
                        = new List<GetRequirementDto>();
                }

                location.RoleOffer.OverallAssigned = volunteers
                       .Where(v => v.RoleOfferId == location.RoleOffer.RoleOfferId
                       && StatusConstants.AssignedNamesList
                       .Any(l => l.ToLower() == v.Status.ToLower()))
                       .Count();

                location.RoleOffer.OverallWaitlisted = volunteers
                .Where(v => v.RoleOfferId == location.RoleOffer.RoleOfferId
                && StatusConstants.WaitlistNamesList
                .Any(l => l.ToLower() == v.Status.ToLower()))
                .Count();

                if (location.RoleOffer.AssigneeDemand != 0)
                {
                    location.RoleOffer.AssigneeDemandPercentage
                        = (location.RoleOffer.OverallAssigned * 100)
                        / location.RoleOffer.AssigneeDemand;
                }
                if (location.RoleOffer.WaitlistDemand != 0)
                {
                    location.RoleOffer.WaitlistDemandPercentage
                       = (location.RoleOffer.OverallWaitlisted * 100)
                       / location.RoleOffer.WaitlistDemand;
                }
            }
           
            return _jsonFactory.CreateJson(StatusCodes.Status200OK,
                null,
                functionalAreaTypes);
        }
    }
}
