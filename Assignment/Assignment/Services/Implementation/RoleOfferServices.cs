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

            IReadOnlyCollection<RoleOffer> dbRoleOffers = (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingWithItemsAsync(r=>!r.IsDeleted))
                .ToList();

            List<RoleOffer> updatedOrAddRoleOffers=new();

            foreach (RoleOffer newExcelRoleOffer in excelRoleOffers)
            {
                RoleOffer? dbRoleOffer = dbRoleOffers
                    .FirstOrDefault(r => r.RoleOfferId == newExcelRoleOffer.RoleOfferId);
                if (dbRoleOffer != null)
                {
                    // if it exists in db then we update
                    dbRoleOffer = RoleOfferCustomMapper
                        .MapExcelRoleOfferToDbRoleOffer(dbRoleOffer, newExcelRoleOffer, 
                        dbRoleOffers ,_mapper);
                }
                else
                {
                    // if the given Roleoffer doesn't exist then check if other items (F.A.,J.T.) 
                    // already exist in db. If yes then set excel items id to db items id
                    FunctionalAreaType? dbExcelEntity = dbRoleOffers
                        .Select(r => r.FunctionalAreaType)
                        .FirstOrDefault(f => f.Name == newExcelRoleOffer.FunctionalAreaType.Name);
                    FunctionalArea? dbFunctionalArea = dbRoleOffers
                        .Select(r => r.FunctionalArea)
                        .FirstOrDefault(f => f.Code == newExcelRoleOffer.FunctionalArea.Code);
                    Location? dbLocation = dbRoleOffers
                        .Select(r => r.Location)
                        .FirstOrDefault(l => l.Code == newExcelRoleOffer.Location.Code);
                    JobTitle? dbJobTitle = dbRoleOffers
                        .Select(r => r.JobTitle)
                        .FirstOrDefault(j => j.Code == newExcelRoleOffer.JobTitle.Code);

                    if (dbLocation != null)
                    {
                        newExcelRoleOffer.Location.Id = dbLocation.Id;
                        newExcelRoleOffer.Location.CreatedAt = dbLocation.CreatedAt;
                    }

                    if (dbExcelEntity!=null)
                    {
                        newExcelRoleOffer.FunctionalAreaType.Id = dbExcelEntity.Id;
                        newExcelRoleOffer.FunctionalAreaType.CreatedAt = dbExcelEntity.CreatedAt;
                    }

                    if (dbFunctionalArea!=null)
                    {
                        newExcelRoleOffer.FunctionalArea.Id = dbFunctionalArea.Id;
                        newExcelRoleOffer.FunctionalArea.CreatedAt = dbFunctionalArea.CreatedAt;
                    }

                    if (dbJobTitle!=null)
                    {
                        newExcelRoleOffer.JobTitle.Id = dbJobTitle.Id;
                        newExcelRoleOffer.JobTitle.CreatedAt = dbJobTitle.CreatedAt;
                    }
                }

                if (newExcelRoleOffer.LevelOfConfidence != null) 
                {
                    newExcelRoleOffer.AssigneeDemand = DemandCalculator
                        .CalculateRoleOfferDemand
                        ((int) newExcelRoleOffer.LevelOfConfidence,
                        newExcelRoleOffer.TotalDemand);
                    newExcelRoleOffer.WaitlistDemand = newExcelRoleOffer.WaitlistDemand;
                }

                updatedOrAddRoleOffers.Add(newExcelRoleOffer);
            }

            List<RoleOffer> removedRoleOffers = new();
            List<FunctionalAreaType> removedFunctionalAreaTypes = new();
            List<Location> removedLocations= new(); 
            List<FunctionalArea> removedFunctionalAreas= new();
            List<JobTitle> removedJobTitles= new();
            
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
                in distinctFunctionalAreaTypes.Where(r=>r.Id == 0))
            {
                ICollection<FunctionalArea> functionalAreas = excelRoleOffers
                    .Where(r => r.FunctionalAreaType.Name == functionalAreaType.Name)
                    .DistinctBy(r => r.FunctionalArea.Name)
                    .Select(r => r.FunctionalArea)
                    .ToList();
                functionalAreaType.FunctionalAreas = functionalAreas;
            }

            foreach (FunctionalArea functionalArea in distinctFunctionalAreas.Where(r => r.Id == 0))
            {
                ICollection<JobTitle> jobTitles = excelRoleOffers
                .Where(r => r.FunctionalArea.Code == functionalArea.Code)
                .DistinctBy(r => r.JobTitle.Code)
                .Select(r => r.JobTitle)
                .ToList();
                functionalArea.JobTitles = jobTitles;
            }

            foreach (JobTitle jobTitle in distinctJobTitles.Where(r=>r.Id == 0))
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
                    .Any(r => r.RoleOfferId == dbRoleOffer.RoleOfferId)
                    &&!removedRoleOffers.Any(r=>r.Id == dbRoleOffer.Id))
                {
                    // Send volunteer request if not sent yet
                    if (!isVolunteersRequestSent)
                    {
                        int[] roleOfferIds = dbRoleOffers
                            .Select(r => r.Id)
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
                    // We don't need the entire RoleOffer since it has other references too
                    // if we add the entire RoleOffer it will cause reference problems
                    removedRoleOffers.Add(new RoleOffer { Id=dbRoleOffer.Id,IsDeleted=true});
                    freeVolunteers.AddRange(
                        volunteersWithRoleOffer.Where
                        (v =>v.RoleOfferId == dbRoleOffer.Id));
                }
                if (!excelRoleOffers
                    .Any(r => r.Location.Code == dbRoleOffer.Location.Code)
                    && !removedLocations.Any(l =>l.Id == dbRoleOffer.Location.Id))
                {
                    removedLocations.Add(new Location 
                    { Id= dbRoleOffer.Location.Id, IsDeleted=true});
                }
                if (!excelRoleOffers
                    .Any(r => r.FunctionalArea.Code == dbRoleOffer.FunctionalArea.Code)
                    && !removedFunctionalAreas.Any(f => f.Id == dbRoleOffer.FunctionalArea.Id))
                {
                    removedFunctionalAreas
                        .Add(new FunctionalArea 
                        { Id = dbRoleOffer.FunctionalArea.Id, IsDeleted = true });
                }
                if (!excelRoleOffers
                    .Any(r => r.FunctionalAreaType.Name == dbRoleOffer.FunctionalAreaType.Name)
                    && !removedFunctionalAreaTypes.Any(f => f.Id == dbRoleOffer.FunctionalAreaType.Id))
                {
                    removedFunctionalAreaTypes
                        .Add(new FunctionalAreaType 
                        { Id = dbRoleOffer.FunctionalAreaType.Id, IsDeleted = true });
                }
                if (!excelRoleOffers
                    .Any(r => r.JobTitle.Code == dbRoleOffer.JobTitle.Code)
                    && !removedJobTitles.Any(j => j.Id == dbRoleOffer.JobTitle.Id))
                {
                    removedJobTitles.Add(new JobTitle 
                    { Id = dbRoleOffer.JobTitle.Id, IsDeleted = true });
                }
            }

            foreach (Volunteer volunteer in freeVolunteers)
            {
                volunteer.RoleOfferId = null;
                volunteer.Status = null;

                // Write to history
                _historyServices.WriteHistory(volunteer,this.Email);
            }

            // remove items that weren't in excel
            // first remove RoleOffers if there is any 
            // Remove it first because it has references to other objects (F.A.T, F.A. etc.)

            if (removedRoleOffers.Count > 0)
            {
                _unitOfWork.RoleOfferRepository.RemoveRangePermanently(removedRoleOffers);
                await _unitOfWork.CompleteAsync();
            }
            _unitOfWork.RoleOfferRepository.UpdateRange(excelRoleOffers);
            await _unitOfWork.CompleteAsync();

            if (removedLocations.Count > 0)
            {
                _unitOfWork.LocationRepository
                    .RemoveRangePermanently(removedLocations);
                await _unitOfWork.CompleteAsync();
            }

            if (removedJobTitles.Count > 0)
            {
                _unitOfWork.JobTitleRepository.RemoveRangePermanently(removedJobTitles);
                await _unitOfWork.CompleteAsync();
            }

            if (removedFunctionalAreas.Count > 0)
            {
                _unitOfWork.FunctionalAreaRepository
                    .RemoveRangePermanently(removedFunctionalAreas);
                await _unitOfWork.CompleteAsync();
            }

            if (removedFunctionalAreaTypes.Count > 0)
            {
                _unitOfWork.FunctionalAreaTypeRepository
                    .RemoveRangePermanently(removedFunctionalAreaTypes);
                await _unitOfWork.CompleteAsync();
            }

            if (freeVolunteers.Count > 0)
                _unitOfWork.VolunteerRepository.UpdateRange(freeVolunteers);

            await _unitOfWork.CompleteAsync();

            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
