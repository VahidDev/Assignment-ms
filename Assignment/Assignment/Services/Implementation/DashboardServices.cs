using Assignment.Constants.VolunteerConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Dtos.DashboardDtos;
using DomainModels.Models.Entities;
using DomainModels.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    internal class DashboardServices : IDashboardServices
    {
        private readonly IJsonFactory _jsonFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardServices
            (IJsonFactory jsonFactory
            , IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _jsonFactory = jsonFactory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ObjectResult> GetAllInfoAsync()
        {
            ICollection<Volunteer> dbVolunteers = (await _unitOfWork.VolunteerRepository
                .GetAllAsNoTrackingAsync(v => !v.IsDeleted )).ToList();
            ICollection<AssigneeDemandWaitlistCountDto> countDtos 
                = (await _unitOfWork.RoleOfferRepository
                .GetAllAssigneeDemandWaitlistCountsAsync(r => !r.IsDeleted)).ToList();
            
            GetAllInfoDto dto = new ()
            {
                OverallAssigneeDemand = countDtos.Sum(r=>r.AssigneeDemand),
                OverallWaitlistDemand = countDtos.Sum(r=>r.WaitlistDemand)
            };

            #region Getting all info
            int acceptedNumber = dbVolunteers
                .Where(v => v.Status != null 
                && v.Status == StatusConstants.Accepted)
                .Count();
            int assignedNumber = dbVolunteers
                .Where(v => v.Status != null
                && v.Status == StatusConstants.Assigned)
                .Count();
            int preAssigned = (dbVolunteers
                .Where(v => v.Status != null
                && v.Status == StatusConstants.PreAssigned)
                .Count());
            int pending = (dbVolunteers
                .Where(v => v.Status != null
                && v.Status == StatusConstants.Pending)
                .Count());

            int waitlistAccepted = (dbVolunteers
              .Where(v => v.Status != null
              && v.Status == StatusConstants.WaitlistAccepted)
              .Count());
            int waitlistAssigned = (dbVolunteers
             .Where(v => v.Status != null 
             && v.Status == StatusConstants.WaitlistAssigned)
             .Count());
            int waitlistOffered = (dbVolunteers
             .Where(v => v.Status != null 
             && v.Status == StatusConstants.WaitlistOffered)
             .Count());
            #endregion 

            #region Setting values
            if (dto.OverallAssigneeDemand != 0)
            {
                dto.Accepted 
                    = Math.Round((double)(acceptedNumber * 100) / dto.OverallAssigneeDemand,2);
                dto.Assigned 
                    = Math.Round((double)(assignedNumber * 100) / dto.OverallAssigneeDemand,2);
                dto.PreAssigned 
                    = Math.Round((double)(preAssigned * 100) / dto.OverallAssigneeDemand,2);
                dto.Pending 
                    = Math.Round((double)(pending * 100) / dto.OverallAssigneeDemand,2);
            }
            if (dto.OverallWaitlistDemand != 0)
            {
                dto.WaitlistAccepted 
                    = Math.Round((double)(waitlistAccepted * 100) / dto.OverallWaitlistDemand,2);
                dto.WaitlistAssigned 
                    = Math.Round((double)(waitlistAssigned * 100) / dto.OverallWaitlistDemand,2);
                dto.WaitlistOffered 
                    = Math.Round((double)(waitlistOffered * 100) / dto.OverallWaitlistDemand,2);
            }
            #endregion

            dto.AssignedRest = 100 
                - dto.Accepted - dto.Assigned - dto.Pending- dto.PreAssigned;
            dto.WaitlistRest = 100 
                - dto.WaitlistOffered - dto.WaitlistAccepted - dto.WaitlistAssigned;

            dto.OverallAssigned 
                = (assignedNumber + acceptedNumber + pending + preAssigned);

            dto.OverallWaitlisted = (waitlistOffered + waitlistAssigned + waitlistAccepted);

            dto.TotalAssigned 
                = (int) (dto.Accepted + dto.Assigned + dto.Pending + dto.PreAssigned);
            dto.TotalWaitlisted
                = (int) (dto.WaitlistOffered + dto.WaitlistAccepted + dto.WaitlistAssigned);

            return _jsonFactory.CreateJson(StatusCodes.Status200OK, null, dto);
        }

        public async Task<ObjectResult> GetRoleOffersAsync(int[] roleOfferIds)
        {
            ICollection<GetRoleOfferDashboardDto> roleOffers = _mapper
                .Map<ICollection<GetRoleOfferDashboardDto>>
                (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingIncludingItemsAsync(r => roleOfferIds.Contains(r.RoleOfferId)));

            if(roleOfferIds.Length != roleOffers.Count)
            {
                return _jsonFactory.CreateJson(StatusCodes.Status404NotFound
                    ,"One of the RoleOffers was not found");
            }

            ICollection<Volunteer> volunteers = (await _unitOfWork.VolunteerRepository
                .GetAllAsNoTrackingAsync(r => r.RoleOfferId != null && !r.IsDeleted
                && roleOfferIds.Contains((int)r.RoleOfferId))).ToList();

            foreach (GetRoleOfferDashboardDto roleOffer in roleOffers)
            {
                roleOffer.Assigned = volunteers
                    .Where(v => v.Status == StatusConstants.Assigned)
                    .Count();
                roleOffer.PreAssigned = volunteers
                    .Where(v => v.Status == StatusConstants.PreAssigned)
                    .Count();
                roleOffer.Accepted = volunteers
                    .Where(v => v.Status == StatusConstants.Accepted)
                    .Count();
                roleOffer.WaitlistAccepted = volunteers
                    .Where(v => v.Status == StatusConstants.WaitlistAccepted)
                    .Count();
                roleOffer.WaitlistAssigned = volunteers
                    .Where(v => v.Status == StatusConstants.WaitlistAssigned)
                    .Count();
                roleOffer.WaitlistOffered = volunteers
                    .Where(v => v.Status == StatusConstants.WaitlistOffered)
                    .Count();
                if (roleOffer.AssigneeDemand != 0)
                {
                    roleOffer.RoleOfferFulfillment
                        = ((roleOffer.Assigned + roleOffer.PreAssigned
                        + roleOffer.Accepted + roleOffer.Pending) * 100)
                        / roleOffer.AssigneeDemand;
                }
                if (roleOffer.WaitlistDemand != 0)
                {
                    roleOffer.WaitlistFulfillment =
                        ((roleOffer.WaitlistOffered + roleOffer.WaitlistAccepted
                        + roleOffer.WaitlistAssigned) * 100) / roleOffer.WaitlistDemand;
                }
            }
            return _jsonFactory.CreateJson(StatusCodes.Status200OK, null, roleOffers);
        }

        public async Task<ObjectResult> GetVolunteersInfoAsync(RoleOfferVolunteerDto dto)
        {
            GetVolunteerInfoDashboardDto dtoToSend = new();

            ICollection<Volunteer> volunteers;

            if (dto.RoleOfferIds.Count() == 0)
            {
                volunteers = (await _unitOfWork.VolunteerRepository
                    .GetAllAsNoTrackingAsync(v=>!v.IsDeleted 
                    && dto.Statuses.Contains(v.Status)
                    && dto.Locations.Contains(v.InternationalVolunteer)))
                    .ToList();
            }
            else
            {
                volunteers = (await _unitOfWork.VolunteerRepository
                   .GetAllAsNoTrackingAsync(v => !v.IsDeleted && v.RoleOfferId != null
                   && dto.RoleOfferIds.Contains((int)v.RoleOfferId)))
                   .ToList();
            }

            dtoToSend.OverallFemales = volunteers
                    .Where(v => dto.Locations.Contains(v.InternationalVolunteer)
                    && v.Gender == GenderEnum.Female.ToString())
                    .Count();
            dtoToSend.OverallMales = volunteers
               .Where(v => dto.Locations.Contains(v.InternationalVolunteer)
               && v.Gender == GenderEnum.Male.ToString())
               .Count();

            foreach (int age in dto.StartingAges)
            {
                dtoToSend.StartingAges.Add(new StartingAgeCountDto
                {
                    Age = age
                ,
                    Count = volunteers
                .Where(v => v.Age > age)
                .Count()
                });
            }

            // Get All distinct countries names
            ICollection<string> countries = volunteers
                .DistinctBy(v => v.Country).Select(v => v.Country).ToList();

            List<CountryNameDto> countryNames = new();

            // Get All country counts
            foreach (string country in countries)
            {
                countryNames.Add(new CountryNameDto
                {
                    Name = country
                    ,
                    Count = volunteers.Where(v => v.Country == country).Count()
                });
            }

            dtoToSend.CountryNameDtos = countryNames
                .OrderByDescending(r => r.Count)
                .Take(dto.CountryCount)
                .ToList();

            dtoToSend.Others = volunteers
                .Where(v => !dtoToSend.CountryNameDtos.Any(c => c.Name == v.Country))
                .Count();

            foreach (AgeRangeDto ageRange in dto.AgeRanges)
            {
                dtoToSend.AgeRanges.Add(new AgeRangeCountDto
                {
                    FromAge = ageRange.FromAge,
                    ToAge = ageRange.ToAge
                ,
                    Count = volunteers
                .Where(v => v.Age > ageRange.FromAge && v.Age < ageRange.ToAge)
                .Count()
                });
            }

            dtoToSend.OverallInternationals = volunteers
                   .Where(v => v.InternationalVolunteer == LocationEnum.International.ToString())
                   .Count();
            dtoToSend.OverallLocals = volunteers
                .Where(v => v.InternationalVolunteer == LocationEnum.Local.ToString())
                .Count();

            return _jsonFactory.CreateJson(StatusCodes.Status200OK,null,dtoToSend);
        }
    }
}
