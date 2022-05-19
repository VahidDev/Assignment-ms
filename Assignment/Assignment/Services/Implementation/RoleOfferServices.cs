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
            List<ExcelEntityDto> entities=_mapper.Map<List<ExcelEntityDto>>
                ((await _unitOfWork.ExcelEntityRepository
                .GetAllAsNoTrackingIncludingItemsAsync(r=>!r.IsDeleted)).ToList());
            ICollection<RoleOfferDto> roleOffers = _mapper
               .Map<ICollection<RoleOfferDto>>(await _unitOfWork.RoleOfferRepository
               .GetAllAsNoTrackingIncludingItemsAsync(e => !e.IsDeleted));
            List<ExcelEntityDto> entitiesToSend = new();

            foreach (ExcelEntityDto excelEntity in entities)
            {
                ExcelEntityDto entity = excelEntity;
                foreach (FunctionalAreaDto functionalArea in entity.FunctionalAreas)
                {
                    foreach (JobTitleDto jobTitle in functionalArea.JobTitles)
                    {
                        List<VenueDto> venues = new();
                        foreach (VenueDto venue in jobTitle.Venues)
                        {
                            NestedRoleOfferDto roleOffer =
                                _mapper.Map<NestedRoleOfferDto>(roleOffers
                                .FirstOrDefault(r => r.Venue.Id == venue.Id
                             && r.ExcelEntity.Id == excelEntity.Id
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
            if(!file.IsFileTypeSupported(FileTypeConstants.ExcelFileContentType))
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
            List<Venue> dbVenues = (await _unitOfWork.VenueRepository
                .GetAllAsNoTrackingAsync(v=>!v.IsDeleted)).ToList();
            List<JobTitle> dbJobTitles = (await _unitOfWork.JobTitleRepository
                .GetAllAsNoTrackingAsync(j=>!j.IsDeleted)).ToList();
            List<ExcelEntity> dbExcelEntities = (await _unitOfWork.ExcelEntityRepository
                .GetAllAsNoTrackingAsync(e=>!e.IsDeleted)).ToList();
            List<FunctionalArea> dbFunctionalAreas = (await _unitOfWork.FunctionalAreaRepository
                .GetAllAsNoTrackingAsync(fa=>!fa.IsDeleted)).ToList();
            List<object>x=new List<object>();
            List<RoleOffer> updatedOrAddedRoleOffers=new();
            foreach (RoleOffer newExcelRoleOffer in excelRoleOffers)
            {
                RoleOffer? dbRoleOffer = dbRoleOffers
                    .FirstOrDefault(r => r.RoleOfferId == newExcelRoleOffer.RoleOfferId);
                Venue? dbVenue = dbVenues
                    .FirstOrDefault(v=> v.ExcelVId== newExcelRoleOffer.Venue.ExcelVId);
                JobTitle? dbJobTitle = dbJobTitles
                    .FirstOrDefault(j => j.ExcelJTId== newExcelRoleOffer.JobTitle.ExcelJTId);
                ExcelEntity? dbExcelEntity = dbExcelEntities
                   .FirstOrDefault(e => e.ExcelEId== newExcelRoleOffer.ExcelEntity.ExcelEId);
                FunctionalArea? dbFunctionalArea = dbFunctionalAreas
                   .FirstOrDefault(f => f.ExcelFAId 
                   == newExcelRoleOffer.FunctionalArea.ExcelFAId);

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
                        newExcelRoleOffer.Venue.Id = dbVenue.Id;
                    }
                    if (dbExcelEntity!=null)
                    {
                        newExcelRoleOffer.ExcelEntity.Id = dbExcelEntity.Id;
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
            List<Venue> removedVenues= new();
            List<FunctionalArea> removedFunctionalAreas= new();
            List<ExcelEntity> removedExcelEntities= new();
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
            foreach (Venue venue in dbVenues)
            {
                if (!updatedOrAddedRoleOffers.Any(r => r.Venue.ExcelVId != venue.ExcelVId))
                {
                    removedVenues.Add(venue);
                }
            }

            foreach (FunctionalArea fa in dbFunctionalAreas)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.FunctionalArea.ExcelFAId != fa.ExcelFAId))
                {
                    removedFunctionalAreas.Add(fa);
                }
            }

            foreach (ExcelEntity entity in dbExcelEntities)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.ExcelEntity.ExcelEId != entity.ExcelEId))
                {
                    removedExcelEntities.Add(entity);
                }
            }

            foreach (JobTitle jobTitle in dbJobTitles)
            {
                if (!updatedOrAddedRoleOffers
                    .Any(r => r.JobTitle.ExcelJTId != jobTitle.ExcelJTId))
                {
                    removedJobTitles.Add(jobTitle);
                }
            }


            List<Venue> sameVenues = updatedOrAddedRoleOffers.
              Select(r => r.Venue)
              .DistinctBy(r => r.ExcelVId)
              .ToList();
            List<FunctionalArea> sameFunctionalAreas = updatedOrAddedRoleOffers.
                Select(r => r.FunctionalArea)
                .DistinctBy(r => r.ExcelFAId).ToList();
            List<ExcelEntity> sameExcelEntity = updatedOrAddedRoleOffers.
                            Select(r => r.ExcelEntity)
                            .DistinctBy(r => r.ExcelEId)
                            .ToList();
            List<JobTitle> sameJobTitles = updatedOrAddedRoleOffers.
                            Select(r => r.JobTitle)
                            .DistinctBy(r => r.ExcelJTId)
                            .ToList();

            List<ExcelEntity> excelEntities = new();
            List<int> roleOfferIds = new();


            //necessary
            foreach (RoleOffer roleOffer in updatedOrAddedRoleOffers)
            {
                ExcelEntity entity = sameExcelEntity
                    .First(r => r.ExcelEId == roleOffer.ExcelEntity.ExcelEId);
                FunctionalArea functionalAreaa = sameFunctionalAreas
                    .First(r => r.ExcelFAId
                    == roleOffer.FunctionalArea.ExcelFAId);
                JobTitle jobTitle = sameJobTitles
                    .First(r => r.ExcelJTId == roleOffer.JobTitle.ExcelJTId);
                Venue venue = sameVenues
                    .First(r => r.ExcelVId == roleOffer.Venue.ExcelVId);

                roleOffer.ExcelEntity = entity;
                roleOffer.FunctionalArea=functionalAreaa;

                roleOffer.JobTitle = jobTitle;
                roleOffer.Venue = venue;
            }

            // Mapping ExcelEntities from Excel
            foreach (ExcelEntity excelEntity in sameExcelEntity)
            {
                ICollection<FunctionalArea>functionalAreas=updatedOrAddedRoleOffers
                    .Where(r=>r.ExcelEntity.ExcelEId==excelEntity.ExcelEId)
                    .DistinctBy(r=>r.FunctionalArea.ExcelFAId)
                    .Select(r=>r.FunctionalArea).ToList();
                excelEntity.FunctionalAreas=functionalAreas;
                foreach (FunctionalArea functionalArea in functionalAreas)
                {
                    ICollection<JobTitle> jobTitles = updatedOrAddedRoleOffers
                    .Where(r => r.FunctionalArea.ExcelFAId== functionalArea.ExcelFAId)
                    .DistinctBy(r => r.JobTitle.ExcelJTId)
                    .Select(r => r.JobTitle).ToList();
                    functionalArea.JobTitles=jobTitles;
                    foreach (JobTitle jobTitle in jobTitles)
                    {
                        ICollection<Venue> venues = updatedOrAddedRoleOffers
                            .Where(r => r.JobTitle.ExcelJTId == jobTitle.ExcelJTId)
                            .DistinctBy(r => r.Venue.ExcelVId)
                            .Select(r => r.Venue).ToList();
                        jobTitle.Venues=venues;
                        foreach (Venue venue in venues)
                        {
                            ICollection<RoleOffer> roleOffers = updatedOrAddedRoleOffers
                            .Where(r => r.Venue.ExcelVId == venue.ExcelVId)
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

            if (removedExcelEntities.Count > 0)
                _unitOfWork.ExcelEntityRepository
                    .RemoveRangePermanently(removedExcelEntities);

            if (removedVenues.Count > 0)
                _unitOfWork.VenueRepository
                    .RemoveRangePermanently(removedVenues);

            if (removedRoleOffers.Count > 0)
                _unitOfWork.RoleOfferRepository.RemoveRangePermanently(removedRoleOffers);

            if(sameExcelEntity.Count>0)
                 _unitOfWork.ExcelEntityRepository.UpdateRange(sameExcelEntity);


            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
