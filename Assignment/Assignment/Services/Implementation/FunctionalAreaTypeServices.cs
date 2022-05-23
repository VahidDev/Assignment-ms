﻿using Assignment.Constants;
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

        public async Task<JsonResult> GetAllIncludingItemsAsync()
        {
            IReadOnlyCollection<RoleOffer>dbRoleOffers=(await _unitOfWork.RoleOfferRepository
                .GetAllIncludingItemsAsync()).ToList();
            List<FunctionalAreaTypeDto> functionalAreaTypes = 
                _mapper.Map<List<FunctionalAreaTypeDto>>(await _unitOfWork
                .FunctionalAreaTypeRepository.GetAllAsNoTrackingIncludingItemsAsync(r=>!r.IsDeleted));
            foreach (RoleOffer roleOffer in dbRoleOffers)
            {
                FunctionalAreaTypeDto functionalAreaType=functionalAreaTypes
                    .First(r=>r.Id==roleOffer.FunctionalAreaType.Id);
                FunctionalAreaDto functionalArea = functionalAreaType.FunctionalAreas
                     .First(r => r.Id == roleOffer.FunctionalArea.Id);
                JobTitleDto jobTitle = functionalArea.JobTitles
                    .First(j => j.Id == roleOffer.JobTitle.Id);
                LocationDto location=jobTitle.Locations
                    .First(l=>l.Id==roleOffer.Location.Id);
                location.RoleOffer=_mapper.Map<NestedRoleOfferDto>(roleOffer);
            }
           
            return _jsonFactory.CreateJson(StatusCodes.Status200OK,
                functionalAreaTypes);
        }
    }
}
