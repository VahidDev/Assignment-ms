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
            return _jsonFactory.CreateJson(StatusCodes.Status200OK,
               _mapper.Map<List<GetFunctionalRequirementDto>>
               (await _unitOfWork.FunctionalRequirementRepository
               .GetAllAsNoTrackingIncludingItemsAsync(fr=>!fr.IsDeleted))
               );
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

            foreach (Requirement requirement in excelRequirements)
            {
                FunctionalRequirement? fr=functionalRequirements
                    .FirstOrDefault(r=>r.ExcelFunctionalRequirementId
                    == requirement.ExcelFunctionalRequirementId);
                if (fr == null)
                {
                    functionalRequirements.Add(new FunctionalRequirement
                    {
                        ExcelFunctionalRequirementId=requirement.ExcelFunctionalRequirementId,
                        Requirements=new List<Requirement>() { requirement } 
                    });
                }
                else
                {
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
