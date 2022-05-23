using Assignment.Constants;
using Assignment.Constants.FileConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using Assignment.Utilities.FileUtilities;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    public class FunctionalRequirementServices : IFunctionalRequirementServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileServices;
        private readonly IJsonFactory _jsonFactory;

        public FunctionalRequirementServices(IUnitOfWork unitOfWork, IMapper mapper
            , IFileServices fileServices, IJsonFactory jsonFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileServices = fileServices;
            _jsonFactory = jsonFactory;
        }

        public async Task<JsonResult> GetAllFunctionalRequirementssync()
        {
            return _jsonFactory.CreateJson(StatusCodes.Status200OK, _mapper
                .Map<List<GetFunctionalRequirementDto>>
                (await _unitOfWork.FunctionalRequirementRepository
                .GetAllAsNoTrackingIncludingItemsAsync(fr => !fr.IsDeleted)));
        }

        public async Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file)
        {
            if (file == null) return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);
            if (!file.IsExcelFile())
            {
                return _jsonFactory.CreateJson(StatusCodes.Status415UnsupportedMediaType,
                    ($"{FileErrorMessageConstants.NotSupportedFile}: " +
                    $"{file.ContentType}"));
            }

            // Get All RoleOffers from excel file
            ICollection<Requirement>? newRequirements = _fileServices
                .ReadCollectionFromExcelFile<Requirement>(file);
            if (newRequirements == null || newRequirements.Count == 0)
            {
                return _jsonFactory
                    .CreateJson(StatusCodes.Status404NotFound,
                    FileErrorMessageConstants.NotInCorrectFormat);
            }
            int[] roleOfferIds = newRequirements.Distinct().Select(x => x.RoleOfferId).ToArray();

            ICollection<RoleOffer> updatedRoleOffers = (await _unitOfWork
                .RoleOfferRepository
                .GetAllSpecificRoleOffers(r=>!r.IsDeleted
                && roleOfferIds.Contains(r.RoleOfferId))).ToList();

            List<FunctionalRequirement> functionalRequirements 
                = (await _unitOfWork.FunctionalRequirementRepository
                .GetAllAsNoTrackingIncludingItemsAsync(fr => !fr.IsDeleted
                && roleOfferIds.Contains(fr.RoleOffer.Id))).ToList();

            foreach (Requirement requirement in newRequirements)
            {
                RoleOffer? roleOffer = updatedRoleOffers
                      .FirstOrDefault(r => r.RoleOfferId == requirement.RoleOfferId);

                if (roleOffer==null)
                    return _jsonFactory
                        .CreateJson(StatusCodes.Status400BadRequest,
                        $"RoleOffer with the ID {requirement.RoleOfferId} was not found");

                FunctionalRequirement? fr=functionalRequirements
                    .FirstOrDefault(r=>r.FunctionalRequirementId
                    == requirement.FunctionalRequirementId);
                if (requirement.WaitlistCount != 0)
                {
                    roleOffer.WaitlistFulfillment
                     = (roleOffer.TotalDemand * 100) / requirement.WaitlistCount;
                }
                if(requirement.LevelOfConfidence != 0)
                {
                    roleOffer.RoleOfferFulfillment
                    = (roleOffer.TotalDemand * 100) / requirement.LevelOfConfidence;
                }
                if (fr == null)
                {
                    functionalRequirements.Add(new FunctionalRequirement
                    {
                        FunctionalRequirementId = requirement.FunctionalRequirementId,
                        Requirements=new List<Requirement>() { requirement } ,
                        RoleOffer=roleOffer
                    });
                }
                else
                {
                    fr.Requirements.Add(requirement);
                }
            }
            _unitOfWork.FunctionalRequirementRepository.UpdateRange(functionalRequirements); 
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
