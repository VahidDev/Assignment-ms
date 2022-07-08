using Assignment.Constants.FileConstants;
using Assignment.Constants.ImportConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using Assignment.Utilities.FileUtilities;
using Assignment.Utilities.ServicesUtilities.RoleOfferUtilities;
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

        public async Task<ObjectResult> GetAllFunctionalRequirementsAsync()
        {
            return _jsonFactory.CreateJson(StatusCodes.Status200OK
                ,null
                ,_mapper.Map<List<GetFunctionalRequirementDto>>
                (await _unitOfWork.FunctionalRequirementRepository
                .GetAllAsNoTrackingIncludingItemsAsync(fr => !fr.IsDeleted)));
        }

        public async Task<ObjectResult> GetByRoleOfferIdAsync(int id)
        {
            FunctionalRequirement functionalRequirement
                = await _unitOfWork.FunctionalRequirementRepository
                .GetByRoleOfferIdAsNoTrackingAsync(id);

            if (functionalRequirement == null)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);
            }

            return _jsonFactory.CreateJson(StatusCodes.Status200OK
                ,null
                ,_mapper.Map<GetFunctionalRequirementDto>(functionalRequirement));
        }

        public async Task<ObjectResult> UpdateOrAddFunctionalRequirementAsync
            (UpdateFunctionalRequirementConvertibleDto convertibleDto)
        {
            UpdateFunctionalRequirementDto dto = _mapper
                .Map<UpdateFunctionalRequirementDto>(convertibleDto);

            FunctionalRequirement dbFunctionalRequirement = new();

            RoleOffer roleOffer = await _unitOfWork.RoleOfferRepository
                .GetByIdAsNoTrackingAsync(dto.RoleOfferId);

            if (roleOffer == null)
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    $"RoleOffer was not found");

            if (dto.Id != null)
            {
                dbFunctionalRequirement 
                    = await _unitOfWork.FunctionalRequirementRepository
                    .GetByIdAsNoTrackingIncludingItemsAsync(fr => fr.Id == dto.Id);

                if(dbFunctionalRequirement == null)
                {
                    return _jsonFactory.CreateJson(StatusCodes.Status404NotFound,
                        $"Functional Requirement was not found");
                }
            }
            else
            {
                dbFunctionalRequirement = new();
            }

            dbFunctionalRequirement.RoleOffer = roleOffer;
            dbFunctionalRequirement.RoleOfferId = roleOffer.RoleOfferId;
            roleOffer.LevelOfConfidence = dto.LevelOfConfidence;
            roleOffer.WaitlistDemand = dto.WaitlistDemand;
            roleOffer.TotalDemand = dto.TotalDemand;
            roleOffer.FunctionalRequirement = dbFunctionalRequirement;  
            dbFunctionalRequirement.RoleOffer.FunctionalRequirement 
                = dbFunctionalRequirement;

            roleOffer.AssigneeDemand
                = DemandCalculator.CalculateRoleOfferDemand
                (dto.LevelOfConfidence, (int)dto.TotalDemand);

            List<UpdateRequirementDto> updatedDtos = new();
            foreach (UpdateRequirementDto updateDto in dto.Requirements)
            {
                if (updateDto.Id != null 
                    && dbFunctionalRequirement.Requirements!=null
                    && !dbFunctionalRequirement.Requirements.Any(r => r.Id == updateDto.Id))
                {
                    return _jsonFactory.CreateJson(StatusCodes.Status404NotFound,
                    $"Requirement {updateDto.Id} was not found");
                }
                if(updatedDtos.Any(r=>r.RequirementName == updateDto.RequirementName
                && r.Operator == updateDto.Operator && r.Value == updateDto.Value))
                {
                    return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                     $"The same requirement for {updateDto.RequirementName} already exists");
                }
                updatedDtos.Add(updateDto);
            }
            if (dbFunctionalRequirement.Requirements != null)
            {
                foreach (Requirement requirement in dbFunctionalRequirement.Requirements)
                {
                    // If any requirement wasn't sent, it means it was removed
                    if (!updatedDtos.Any(r => r.Id == requirement.Id))
                    {
                        requirement.IsDeleted = true;
                        updatedDtos.Add(_mapper.Map<UpdateRequirementDto>(requirement));
                    }
                }
            }
            dbFunctionalRequirement.Requirements = _mapper
                .Map<ICollection<Requirement>>(updatedDtos);

            _unitOfWork.FunctionalRequirementRepository.Update(dbFunctionalRequirement);
            _unitOfWork.RoleOfferRepository.Update(roleOffer);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<ObjectResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file)
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
                    .CreateJson(StatusCodes.Status400BadRequest,
                    FileErrorMessageConstants.NotInCorrectFormat);
            }

            int[] roleOfferIds = newRequirements
                .DistinctBy(r=>r.RoleOfferId)
                .Select(x => x.RoleOfferId)
                .ToArray();

            ICollection<RoleOffer> updatedRoleOffers = (await _unitOfWork
                .RoleOfferRepository
                .GetAllAsNoTrackingAsync(r=>!r.IsDeleted
                && roleOfferIds.Contains(r.RoleOfferId))).ToList();

            ICollection<FunctionalRequirement> functionalRequirements 
                = await _unitOfWork.FunctionalRequirementRepository 
                .GetAllAsNoTrackingIncludingItemsAsync(r=>!r.IsDeleted 
                && roleOfferIds.Contains(r.RoleOfferId));

            foreach (Requirement requirement in newRequirements)
            {
                if(requirement.Value == FRImportConstants.NullConstant)
                {
                    requirement.Value = null;
                }

                RoleOffer? roleOffer = updatedRoleOffers
                      .FirstOrDefault(r => r.RoleOfferId == requirement.RoleOfferId);

                if (roleOffer == null)
                {
                    return _jsonFactory
                        .CreateJson(StatusCodes.Status400BadRequest,
                        $"RoleOffer with the ID {requirement.RoleOfferId} was not found");
                }
                FunctionalRequirement? fr = functionalRequirements
                    .FirstOrDefault(r=>r.RoleOffer.RoleOfferId == requirement.RoleOfferId);
                
                if(fr != null && fr.Requirements
                    .Any(r=>r.RequirementName == requirement.RequirementName
                    && r.Operator == requirement.Operator && r.Value == requirement.Value))
                {
                    return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                        $"The same requirement for {requirement.RequirementName} already exists");
                }

                if (fr == null)
                {
                    functionalRequirements.Add(new FunctionalRequirement
                    {
                        Requirements = new List<Requirement>() { requirement },
                        RoleOffer = roleOffer,
                        RoleOfferId = roleOffer.RoleOfferId
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
