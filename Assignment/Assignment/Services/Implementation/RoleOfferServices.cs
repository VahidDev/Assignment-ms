using Assignment.Constants.FileConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using Assignment.Utilities.FileUtilities;
using Assignment.Utilities.ServicesUtilities.MapperUtilities;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using DomainModels.Models.Enums;
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

        public RoleOfferServices(IUnitOfWork unitOfWork, IMapper mapper
            ,IFileServices fileServices, IJsonFactory jsonFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileServices = fileServices;
            _jsonFactory = jsonFactory;
        }
        public async Task<JsonResult> GetALlNestedRoleOffersAsync()
        {
            List<FunctionalAreaTypeDto> entities=_mapper.Map<List<FunctionalAreaTypeDto>>
                ((await _unitOfWork.FunctionalAreaTypeRepository
                .GetAllAsNoTrackingIncludingItemsAsync(r=>!r.IsDeleted))
                .ToList());
            ICollection<RoleOfferDto> roleOffers = _mapper
               .Map<ICollection<RoleOfferDto>>(await _unitOfWork.RoleOfferRepository
               .GetAllAsNoTrackingIncludingItemsAsync(e => !e.IsDeleted));


            List<FunctionalAreaTypeDto> entitiesToSend = new();

            foreach (FunctionalAreaTypeDto excelEntity in entities)
            {
                FunctionalAreaTypeDto entity = excelEntity;
                foreach (FunctionalAreaDto functionalArea in entity.FunctionalAreas)
                {
                    foreach (JobTitleDto jobTitle in functionalArea.JobTitles)
                    {
                        List<LocationDto> venues = new();
                        foreach (LocationDto venue in jobTitle.Venues)
                        {
                            NestedRoleOfferDto roleOffer =
                                _mapper.Map<NestedRoleOfferDto>(roleOffers
                                .FirstOrDefault(r => r.Location.Id == venue.Id
                             && r.FunctionalAreaType.Id == excelEntity.Id
                             && r.JobTitle.Id == jobTitle.Id
                             && r.FunctionalArea.Id == functionalArea.Id));
                            if (roleOffer==null)
                                continue;
                            venue.RoleOffer = roleOffer;
                            venues.Add(venue);
                        }
                        jobTitle.Venues = venues;
                    }
                    
                }
                entitiesToSend.Add(entity);
            }


            return _jsonFactory.CreateJson(StatusCodes.Status200OK, entitiesToSend);
        }

        public async Task<JsonResult> GetALlRoleOffersAsync()
        {
           ICollection<RoleOfferDto> roleOffers=_mapper
                .Map<ICollection<RoleOfferDto>>(await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingIncludingItemsAsync(e=>!e.IsDeleted));

            return _jsonFactory.CreateJson(StatusCodes.Status200OK, roleOffers);
        }

        public async Task<JsonResult> GetRoleOfferAsync(int id)
        {
            RoleOffer roleOffer =await _unitOfWork.RoleOfferRepository
                .FirstOrDefaultIncludingItemsAsync(r => r.Id == id && !r.IsDeleted);
            if (roleOffer == null) return _jsonFactory
                    .CreateJson(StatusCodes.Status404NotFound);
            return _jsonFactory.CreateJson(StatusCodes.Status200OK, roleOffer);
        }

        public async Task<JsonResult> ValidateExcelFileThenWriteToDbAsync(IFormFile file)
        {
            if (file == null) return _jsonFactory.CreateJson(StatusCodes.Status404NotFound);
            if(!file.IsExcelFile())
            {
                return _jsonFactory.CreateJson(StatusCodes.Status415UnsupportedMediaType,
                    ($"{FileErrorMessageConstants.NotSupportedFile}: " +
                    $"{file.ContentType}"));
            }
            // Get All RoleOffers from excel file
            ICollection<RoleOffer> excelRoleOffers = _fileServices
                .ReadCollectionFromExcelFile<RoleOffer>(file);

            List<RoleOffer> dbRoleOffers = (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingIncludingItemsAsync(r => !r.IsDeleted)).ToList();

            List<Location> dbLocations = (await _unitOfWork.LocationRepository
                .GetAllAsNoTrackingAsync(v=>!v.IsDeleted)).ToList();

            List<JobTitle> dbJobTitles = (await _unitOfWork.JobTitleRepository
                .GetAllAsNoTrackingAsync(j=>!j.IsDeleted)).ToList();

            List<FunctionalAreaType> dbFunctionalAreaType
                = (await _unitOfWork.FunctionalAreaTypeRepository
                .GetAllAsNoTrackingAsync(e=>!e.IsDeleted)).ToList();

            List<FunctionalArea> dbFunctionalAreas 
                = (await _unitOfWork.FunctionalAreaRepository
                .GetAllAsNoTrackingAsync(fa=>!fa.IsDeleted)).ToList();

            List<object>x=new List<object>();
            List<RoleOffer> updatedOrAddedRoleOffers=new();
            foreach (RoleOffer newExcelRoleOffer in excelRoleOffers)
            {
                RoleOffer? dbRoleOffer = dbRoleOffers
                    .FirstOrDefault(r => r.RoleOfferId == newExcelRoleOffer.RoleOfferId);
                Location? dbVenue = dbLocations
                    .FirstOrDefault(v=> v.Code== newExcelRoleOffer.Location.Code);
                JobTitle? dbJobTitle = dbJobTitles
                    .FirstOrDefault(j => j.Code== newExcelRoleOffer.JobTitle.Code);
                FunctionalAreaType? dbExcelEntity = dbFunctionalAreaType
                   .FirstOrDefault(e => e.Name== newExcelRoleOffer.FunctionalAreaType.Name);
                FunctionalArea? dbFunctionalArea = dbFunctionalAreas
                   .FirstOrDefault(f => f.Code == newExcelRoleOffer.FunctionalArea.Code);

                if (dbRoleOffer != null)
                {
                    // if it exists in db then we update
                    RoleOfferCustomMapper
                        .MapDbRoleOfferToExcelRoleOfferId
                        (ref dbRoleOffer, newExcelRoleOffer, _mapper,dbVenue,dbJobTitle
                        ,dbExcelEntity,dbFunctionalArea);
                }
                else
                {
                    if (dbVenue!=null)
                    {
                        newExcelRoleOffer.Location.Id = dbVenue.Id;
                    }
                    if (dbExcelEntity!=null)
                    {
                        newExcelRoleOffer.FunctionalAreaType.Id = dbExcelEntity.Id;
                    }
                    if (dbFunctionalArea!=null)
                    {
                        newExcelRoleOffer.FunctionalArea.Id = dbFunctionalArea.Id;
                    }
                    if (dbJobTitle!=null)
                    {
                        newExcelRoleOffer.JobTitle.Id = dbJobTitle.Id;
                    }
                }
                updatedOrAddedRoleOffers.Add(newExcelRoleOffer);
            }
            List<RoleOffer> removedRoleOffers = new();
            List<Volunteer> volunteersWithRemovedRoleOffer= new();
            List<Location> removedLocations= new();
            List<FunctionalArea> removedFunctionalAreas= new();
            List<FunctionalAreaType> removedFunctionalAreaTypes = new();
            List<JobTitle> removedJobTitles= new();

            //collecting removed roleoffers and others 
            foreach (RoleOffer dbRoleOffer in dbRoleOffers)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.RoleOfferId == dbRoleOffer.RoleOfferId))
                {
                    removedRoleOffers.Add(dbRoleOffer);
                    volunteersWithRemovedRoleOffer.AddRange(await _unitOfWork.VolunteerRepository
                        .GetAllAsNoTrackingAsync
                        (v =>v.RoleOfferId == dbRoleOffer.Id && !v.IsDeleted &&
                        (v.Status == Statusenum.Assigned 
                        || v.Status == Statusenum.Waitlisted)));
                }
            }
            foreach (Volunteer volunteer in volunteersWithRemovedRoleOffer)
            {
                volunteer.RoleOfferId = null;
                volunteer.Status = Statusenum.Free;
            }
            foreach (Location location in dbLocations)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.Location.Code != location.Code))
                {
                    removedLocations.Add(location);
                }
            }

            foreach (FunctionalArea fa in dbFunctionalAreas)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.FunctionalArea.Name != fa.Name))
                {
                    removedFunctionalAreas.Add(fa);
                }
            }

            foreach (FunctionalAreaType functionalAreaType in dbFunctionalAreaType)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.FunctionalAreaType.Name!= functionalAreaType.Name))
                {
                    removedFunctionalAreaTypes.Add(functionalAreaType);
                }
            }

            foreach (JobTitle jobTitle in dbJobTitles)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.JobTitle.Code != jobTitle.Code))
                {
                    removedJobTitles.Add(jobTitle);
                }
            }


            List<Location> sameLocations = updatedOrAddedRoleOffers.
              Select(r => r.Location)
              .DistinctBy(r => r.Code)
              .ToList();
            List<FunctionalArea> sameFunctionalAreas = updatedOrAddedRoleOffers.
                Select(r => r.FunctionalArea)
                .DistinctBy(r => r.Code).ToList();
            List<FunctionalAreaType> sameFunctionalAreaTypes= updatedOrAddedRoleOffers.
                            Select(r => r.FunctionalAreaType)
                            .DistinctBy(r => r.Name)
                            .ToList();
            List<JobTitle> sameJobTitles = updatedOrAddedRoleOffers.
                            Select(r => r.JobTitle)
                            .DistinctBy(r => r.Code)
                            .ToList();

            List<FunctionalAreaType> functionalAreaTypes = new();
            List<int> roleOfferIds = new();


            //necessary
            foreach (RoleOffer roleOffer in updatedOrAddedRoleOffers)
            {
                FunctionalAreaType functionalAreaType = sameFunctionalAreaTypes
                    .First(r => r.Name == roleOffer.FunctionalAreaType.Name);
                FunctionalArea functionalAreaa = sameFunctionalAreas
                    .First(r => r.Code
                    == roleOffer.FunctionalArea.Code);
                JobTitle jobTitle = sameJobTitles
                    .First(r => r.Code == roleOffer.JobTitle.Code);
                Location location = sameLocations
                    .First(r => r.Code == roleOffer.Location.Code);

                roleOffer.FunctionalAreaType = functionalAreaType;
                roleOffer.FunctionalArea=functionalAreaa;

                roleOffer.JobTitle = jobTitle;
                roleOffer.Location = location;
            }

            // Mapping ExcelEntities from Excel
            foreach (FunctionalAreaType excelEntity in sameFunctionalAreaTypes)
            {
                ICollection<FunctionalArea>functionalAreas=updatedOrAddedRoleOffers
                    .Where(r=>r.FunctionalAreaType.Name==excelEntity.Name)
                    .DistinctBy(r=>r.FunctionalArea.Name)
                    .Select(r=>r.FunctionalArea)
                    .ToList();
                excelEntity.FunctionalAreas=functionalAreas;
                foreach (FunctionalArea functionalArea in functionalAreas)
                {
                    ICollection<JobTitle> jobTitles = updatedOrAddedRoleOffers
                    .Where(r => r.FunctionalArea.Code== functionalArea.Code)
                    .DistinctBy(r => r.JobTitle.Code)
                    .Select(r => r.JobTitle)
                    .ToList();
                    functionalArea.JobTitles=jobTitles;
                    foreach (JobTitle jobTitle in jobTitles)
                    {
                        ICollection<Location> venues = updatedOrAddedRoleOffers
                            .Where(r => r.JobTitle.Code == jobTitle.Code)
                            .DistinctBy(r => r.Location.Code)
                            .Select(r => r.Location)
                            .ToList();
                        jobTitle.Venues=venues;
                        foreach (Location venue in venues)
                        {
                            ICollection<RoleOffer> roleOffers = updatedOrAddedRoleOffers
                            .Where(r => r.Location.Code == venue.Code)
                            .DistinctBy(r => r.RoleOfferId)
                            .ToList();
                            venue.RoleOffers = roleOffers;
                        }
                    }
                }
            }

            if (volunteersWithRemovedRoleOffer.Count > 0)
                _unitOfWork.VolunteerRepository.UpdateRange(volunteersWithRemovedRoleOffer);

            if (removedJobTitles.Count > 0)
                _unitOfWork.JobTitleRepository.RemoveRangePermanently(removedJobTitles);

            if (removedFunctionalAreas.Count > 0)
                _unitOfWork.FunctionalAreaRepository
                    .RemoveRangePermanently(removedFunctionalAreas);

            if (removedFunctionalAreaTypes.Count > 0)
                _unitOfWork.FunctionalAreaTypeRepository
                    .RemoveRangePermanently(removedFunctionalAreaTypes);

            if (removedLocations.Count > 0)
                _unitOfWork.LocationRepository
                    .RemoveRangePermanently(removedLocations);

            if (removedRoleOffers.Count > 0)
                _unitOfWork.RoleOfferRepository.RemoveRangePermanently(removedRoleOffers);

            if(sameFunctionalAreaTypes.Count>0)
                 await _unitOfWork.FunctionalAreaTypeRepository
                    .AddRangeAsync(sameFunctionalAreaTypes);


            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
