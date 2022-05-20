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
            List<FunctionalRequirement> requirementDtos = _mapper
                .Map<List<FunctionalRequirement>>
                (await _unitOfWork.FunctionalRequirementRepository
                .GetAllAsNoTrackingIncludingItemsAsync(fr => !fr.IsDeleted));
            List<GetFunctionalRequirementDto> dtoToSend = new();

            foreach (FunctionalRequirement fr in requirementDtos)
            {
                GetFunctionalRequirementDto dto = new();
                dto.Requirements = new List<GetRequirementDto>();
                foreach (Requirement requirement in fr.Requirements)
                {
                    GetRequirementDto requirementDto=_mapper
                        .Map<GetRequirementDto>(requirement);
                    requirementDto.Value = requirement.Value
                        .Split(DbValueSeperatorConstants.TripleDashSeperator);
                    dto.Requirements.Add(requirementDto);
                }
                dto.Id=fr.Id;
                dtoToSend.Add(dto);
            }
            return _jsonFactory.CreateJson(StatusCodes.Status200OK, dtoToSend);
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
            ICollection<Requirement> excelRequirements = _fileServices
                .ReadCollectionFromExcelFile<Requirement>(file);
            List<FunctionalRequirement> functionalRequirements = new();

            int[] roleOfferIds = excelRequirements.Select(x => x.RoleOfferId).ToArray();
            ICollection<RoleOffer> updatedRoleOffers = (await _unitOfWork
                .RoleOfferRepository
                .GetAllSpecificRoleOffers(r=>!r.IsDeleted&&roleOfferIds.Contains(r.Id)))
                .ToList();

            foreach (Requirement requirement in excelRequirements)
            {
                RoleOffer? roleOffer = updatedRoleOffers
                      .FirstOrDefault(r => r.Id == requirement.RoleOfferId);
                if (roleOffer==null)
                    return _jsonFactory
                        .CreateJson(StatusCodes.Status400BadRequest
                        , $"Role Offer {requirement.RoleOfferId} was not found");

                FunctionalRequirement? fr=functionalRequirements
                    .FirstOrDefault(r=>r.ExcelFunctionalRequirementId
                    == requirement.ExcelFunctionalRequirementId);
                if (fr == null)
                {
                    roleOffer.WaitListCount = requirement.WaitListCount;
                    roleOffer.LevelOfConfidence = requirement.LevelOfConfidence;
                    functionalRequirements.Add(new FunctionalRequirement
                    {
                        ExcelFunctionalRequirementId=requirement.ExcelFunctionalRequirementId,
                        Requirements=new List<Requirement>() { requirement } ,
                        RoleOffer=roleOffer
                    });
                }
                else
                {
                    fr.RoleOfferId = requirement.RoleOfferId;
                    fr.Requirements.Add(requirement);
                }
            }
            await _unitOfWork.FunctionalRequirementRepository
                .AddRangeAsync(functionalRequirements); 
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
