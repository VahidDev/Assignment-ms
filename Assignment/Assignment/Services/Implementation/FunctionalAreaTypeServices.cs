﻿using Assignment.Constants.VolunteerConstants;
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
            IReadOnlyCollection<RoleOffer>dbRoleOffers=(await _unitOfWork.RoleOfferRepository
                .GetAllIncludingItemsAsync()).ToList();
            List<FunctionalAreaTypeDto> functionalAreaTypes = 
                _mapper.Map<List<FunctionalAreaTypeDto>>(await _unitOfWork
                .FunctionalAreaTypeRepository
                .GetAllWithItemsAsNoTrackingAsync(r=>!r.IsDeleted));

            if(functionalAreaTypes.Count == 0)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status200OK,null,functionalAreaTypes);
            }

            IReadOnlyCollection<Volunteer> volunteers = (await _unitOfWork.VolunteerRepository
                .GetAllAsNoTrackingAsync(r => r.RoleOfferId != null && !r.IsDeleted)).ToList();
            foreach (RoleOffer roleOffer in dbRoleOffers)
            {
                FunctionalAreaTypeDto functionalAreaType=functionalAreaTypes
                    .First(r=>r.Id == roleOffer.FunctionalAreaType.Id);
                FunctionalAreaDto functionalArea = functionalAreaType.FunctionalAreas
                     .First(r => r.Id == roleOffer.FunctionalArea.Id);
                JobTitleDto jobTitle = functionalArea.JobTitles
                    .First(j => j.Id == roleOffer.JobTitle.Id);
                LocationDto location=jobTitle.Locations
                    .First(l=>l.Id == roleOffer.Location.Id);

                location.RoleOffer=_mapper.Map<NestedRoleOfferDto>(roleOffer);

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
