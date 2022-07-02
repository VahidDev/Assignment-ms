using Assignment.Constants.FileConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using Assignment.Utilities.FileUtilities;
using Assignment.Utilities.ServicesUtilities.MapperUtilities;
using Assignment.Utilities.ServicesUtilities.RoleOfferUtilities;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Dtos.RoleOffeerDtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    internal class RoleOfferServices : IRoleOfferServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileServices;
        private readonly IJsonFactory _jsonFactory;
        private readonly IHistoryServices _historyServices;
        
        public string? Email { get; set; }

        public RoleOfferServices
            (IUnitOfWork unitOfWork
            , IMapper mapper
            , IFileServices fileServices
            , IJsonFactory jsonFactory
            , IHistoryServices historyServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileServices = fileServices;
            _jsonFactory = jsonFactory;
            _historyServices = historyServices;
        }

        public async Task<ObjectResult> GetAllRoleOffersAsync()
        {
           ICollection<RoleOfferDto> roleOffers=_mapper
                .Map<ICollection<RoleOfferDto>>(await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingWithItemsAsync(e=>!e.IsDeleted));

            return _jsonFactory.CreateJson(StatusCodes.Status200OK,null, roleOffers);
        }

        public async Task<ObjectResult> GetRoleOfferAsync(int id)
        {
            RoleOffer roleOffer =await _unitOfWork.RoleOfferRepository
                .FirstOrDefaultIncludingItemsAsync(r => r.Id == id && !r.IsDeleted);
            if (roleOffer == null) return _jsonFactory
                    .CreateJson(StatusCodes.Status404NotFound);
            return _jsonFactory.CreateJson(
                StatusCodes.Status200OK,
                null,
                _mapper.Map<RoleOfferDto>(roleOffer));
        }

        public async Task<ObjectResult> ImportRoleOfferDetailsAsync(IFormFile file)
        {
            if (file == null) return _jsonFactory
                    .CreateJson(StatusCodes.Status404NotFound,"The file is not provided");

            if (!file.IsExcelFile())
            {
                return _jsonFactory.CreateJson(StatusCodes.Status415UnsupportedMediaType,
                    ($"{FileErrorMessageConstants.NotSupportedFile}: " +
                    $"{file.ContentType}"));
            }
            ICollection<ImportRoleOfferDetailsDto>? dtos = _fileServices
                .ReadCollectionFromExcelFile<ImportRoleOfferDetailsDto>(file);

            if (dtos == null || dtos.Count == 0)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    FileErrorMessageConstants.NotInCorrectFormat);
            }

            int[] roleOfferIds = dtos.Select(r => r.RoleOfferId).Distinct().ToArray();
            IReadOnlyCollection<RoleOffer> updatedRoleOffers 
                = (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingAsync(r =>roleOfferIds
                .Contains(r.RoleOfferId)))
                .ToList();

            if (updatedRoleOffers.Count != roleOfferIds.Length)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound,
                    "Role offer couldn't be found");
            }

            foreach (RoleOffer updatedRoleOffer in updatedRoleOffers)
            {
                ImportRoleOfferDetailsDto dto 
                    = dtos.First(r => r.RoleOfferId == updatedRoleOffer.RoleOfferId);

                updatedRoleOffer.AssigneeDemand = DemandCalculator
                    .CalculateRoleOfferDemand(dto.LevelOfConfidence, 
                    updatedRoleOffer.TotalDemand);

                updatedRoleOffer.WaitlistDemand = dto.WaitlistDemand;
                updatedRoleOffer.LevelOfConfidence = dto.LevelOfConfidence;
            }

            _unitOfWork.RoleOfferRepository.UpdateRange(updatedRoleOffers);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }

        public async Task<ObjectResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file)
        {
            if (file == null) return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);
            if(!file.IsExcelFile())
            {
                return _jsonFactory.CreateJson(StatusCodes.Status415UnsupportedMediaType,
                    ($"{FileErrorMessageConstants.NotSupportedFile}: " +
                    $"{file.ContentType}"));
            }
            // Get All RoleOffers from excel file
            ICollection<RoleOffer>? excelRoleOffers = _fileServices
                .ReadCollectionFromExcelFile<RoleOffer>(file);

            if (excelRoleOffers == null || excelRoleOffers.Count == 0)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    FileErrorMessageConstants.NotInCorrectFormat);
            }

            ICollection<RoleOffer> dbRoleOffers 
                = (await _unitOfWork.RoleOfferRepository
                .GetAllAsync())
                .ToList();


            //ICollection<FunctionalRequirement> functionalRequirements
            //    = (await _unitOfWork.FunctionalRequirementRepository
            //    .GetAllAsNoTrackingAsync(r=>!r.IsDeleted))
            //    .ToList();

            //foreach (FunctionalRequirement functionalRequirement in functionalRequirements)
            //{
            //    RoleOffer roleOffer = dbRoleOffers
            //        .First(r => r.Id == functionalRequirement.RoleOfferId);

            //    functionalRequirement.RoleOfferId = roleOffer.RoleOfferId;
            //}
            //_unitOfWork.FunctionalRequirementRepository.UpdateRange(functionalRequirements);
            //await _unitOfWork.CompleteAsync();

            foreach (RoleOffer excelRoleOffer in excelRoleOffers)
            {
                if (excelRoleOffer.LevelOfConfidence != null) 
                {
                    excelRoleOffer.AssigneeDemand = DemandCalculator
                        .CalculateRoleOfferDemand
                        ((int)excelRoleOffer.LevelOfConfidence,
                        excelRoleOffer.TotalDemand);
                }
            }
            
            List<Volunteer> freeVolunteers= new();
            List<Volunteer> volunteersWithRoleOffer= new();

            List<FunctionalAreaType> distinctFunctionalAreaTypes = excelRoleOffers
             .Select(r => r.FunctionalAreaType)
             .DistinctBy(r => r.Name)
             .ToList();
            List<FunctionalArea> distinctFunctionalAreas = excelRoleOffers
               .Select(r => r.FunctionalArea)
               .DistinctBy(r => r.Code)
               .ToList();
            List<Location> distinctLocations = excelRoleOffers
                .Select(r => r.Location)
                .DistinctBy(r => r.Code)
                .ToList();
            List<JobTitle> distinctJobTitles = excelRoleOffers
                .Select(r => r.JobTitle)
                .DistinctBy(r => r.Code)
                .ToList();

            // Setting the fields of RoleOffer. The fields are F.A.T., F.A., J.T. etc.
            foreach (RoleOffer roleOffer in excelRoleOffers)
            {
                FunctionalAreaType functionalAreaType = distinctFunctionalAreaTypes
                    .First(f => f.Name == roleOffer.FunctionalAreaType.Name);
                FunctionalArea functionalArea = distinctFunctionalAreas
                    .First(f => f.Code == roleOffer.FunctionalArea.Code);
                JobTitle jobTitle = distinctJobTitles
                    .First(j => j.Code == roleOffer.JobTitle.Code);
                Location location = distinctLocations
                    .First(l => l.Code == roleOffer.Location.Code);

                roleOffer.FunctionalAreaType = functionalAreaType;
                roleOffer.FunctionalArea = functionalArea;

                roleOffer.JobTitle = jobTitle;
                roleOffer.Location = location;  
            }

            // these loops are for the EF to help it understand the relations
            // for example, these FuntionalAreaTypes have these FunctionalAreas
            // Thus we need to set them so that EF understands
            // Also note that we are only doing this for the new ones so that
            // the realations can be created. If the relations already exist in db EF will understand it
            // So we do it only for the new ones (the ones that don't have id)

            foreach (FunctionalAreaType functionalAreaType
                in distinctFunctionalAreaTypes)
            {
                ICollection<FunctionalArea> functionalAreas = excelRoleOffers
                    .Where(r => r.FunctionalAreaType.Name == functionalAreaType.Name)
                    .DistinctBy(r => r.FunctionalArea.Name)
                    .Select(r => r.FunctionalArea)
                    .ToList();
                functionalAreaType.FunctionalAreas = functionalAreas;
            }

            foreach (FunctionalArea functionalArea in distinctFunctionalAreas)
            {
                ICollection<JobTitle> jobTitles = excelRoleOffers
                .Where(r => r.FunctionalArea.Code == functionalArea.Code)
                .DistinctBy(r => r.JobTitle.Code)
                .Select(r => r.JobTitle)
                .ToList();
                functionalArea.JobTitles = jobTitles;
            }

            foreach (JobTitle jobTitle in distinctJobTitles)
            {
                ICollection<Location> locations = excelRoleOffers
                    .Where(r => r.JobTitle.Code == jobTitle.Code)
                    .DistinctBy(r => r.Location.Code)
                    .Select(r => r.Location)
                    .ToList();
                jobTitle.Locations = locations;
            }

            // isVolunteersRequestSent variable is used for perfomance improvement purposes
            // if one RoleOffer is not found in excel then this means that 
            // this RoleOffer has to be removed and volunteers with this RoleOffer
            // need to be set free. Therefore, in this endpoint we also request for volunteers 
            // but bringing all the volunteers at once without checking
            // if even one removal of RoleOffer actually happened causes poor perfomance 
            // so this variable determines if the volunteers already brought from db or not
            bool isVolunteersRequestSent = false;
            foreach (RoleOffer dbRoleOffer in dbRoleOffers)
            {
                if (!excelRoleOffers
                    .Any(r => r.RoleOfferId == dbRoleOffer.RoleOfferId))
                {
                    // Send volunteer request if not sent yet
                    if (!isVolunteersRequestSent)
                    {
                        int[] roleOfferIds = dbRoleOffers
                            .Select(r => r.RoleOfferId)
                            .ToArray();
                        volunteersWithRoleOffer = (
                            await _unitOfWork.VolunteerRepository
                            .GetAllAsNoTrackingAsync
                            (v => v.RoleOfferId != null && !v.IsDeleted
                            && roleOfferIds
                            .Contains((int)v.RoleOfferId)))
                            .ToList();
                        isVolunteersRequestSent = true;
                    }
                    freeVolunteers.AddRange(
                        volunteersWithRoleOffer.Where
                        (v =>v.RoleOfferId == dbRoleOffer.RoleOfferId));
                }
            }

            foreach (Volunteer volunteer in freeVolunteers)
            {
                volunteer.RoleOfferId = null;
                volunteer.Status = null;

                // Write to history
                _historyServices.WriteHistory(volunteer,this.Email);
            }

            _unitOfWork.Complete();

            // remove items that weren't in excel
            // first remove RoleOffers if there is any 
            // Remove it first because it has references to other objects (F.A.T, F.A. etc.)

             _unitOfWork.RoleOfferRepository
                .RemoveRangePermanently(dbRoleOffers);
            await _unitOfWork.CompleteAsync();

            List<FunctionalAreaType> removedFunctionalAreaTypes
                = (await _unitOfWork.FunctionalAreaTypeRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted))
                .ToList();

            List<FunctionalArea> removedFunctionalAreas
                = (await _unitOfWork.FunctionalAreaRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted))
                .ToList();

            List<JobTitle> removedJobTitles
                = (await _unitOfWork.JobTitleRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted))
                .ToList();

            List<Location> removedLocations
               = (await _unitOfWork.LocationRepository
               .GetAllAsNoTrackingAsync(r => !r.IsDeleted))
               .ToList();

            _unitOfWork.FunctionalAreaTypeRepository
                .RemoveRangePermanently(removedFunctionalAreaTypes);

            _unitOfWork.FunctionalAreaRepository
                .RemoveRangePermanently(removedFunctionalAreas);
            
            _unitOfWork.JobTitleRepository
                .RemoveRangePermanently(removedJobTitles);

            _unitOfWork.LocationRepository
                .RemoveRangePermanently(removedLocations);

            if (freeVolunteers.Count > 0)
            {
                _unitOfWork.VolunteerRepository
                    .UpdateRange(freeVolunteers);
            }

            await _unitOfWork.RoleOfferRepository
                .AddRangeAsync(excelRoleOffers);
            
            await _unitOfWork.CompleteAsync();

            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
