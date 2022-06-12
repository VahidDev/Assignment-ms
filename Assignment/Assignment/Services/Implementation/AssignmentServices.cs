using Assignment.Constants.VolunteerConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;
namespace Assignment.Services.Implementation
{
    internal class AssignmentServices : IAssignmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJsonFactory _jsonFactory;
        private readonly IHistoryServices _historyServices;
        public AssignmentServices
            (IUnitOfWork unitOfWork
            , IMapper mapper
            , IJsonFactory jsonFactory
            , IHistoryServices historyServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jsonFactory = jsonFactory;
            _historyServices = historyServices;
        }

        public async Task<ObjectResult> AssignOrWaitlistAsync
            (ICollection<AssignOrWaitlistVolunteerDto> volunteerDtos, string email)
        {
            if (volunteerDtos.Count == 0)
                return _jsonFactory.CreateJson(StatusCodes.Status204NoContent);

            // Check if all statuses are valid
            if (volunteerDtos
                .Any(v=>v.Status.ToLower() != StatusConstants.PreAssigned.ToLower()
                && v.Status.ToLower() != StatusConstants.WaitlistOffered.ToLower()))
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest,
                    "Invalid Status");
            }

            ICollection<Volunteer> dbVolunteers = (await _unitOfWork.VolunteerRepository
                .GetAllAsNoTrackingAsync(v=> !v.IsDeleted && volunteerDtos
                .Select(d=>d.Id)
                .ToArray()
                .Contains(v.CandidateId)))
                .ToList();
            ICollection<RoleOffer>dbRoleOffers = (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingAsync(r=>!r.IsDeleted 
                && volunteerDtos.Select(d=>d.RoleOfferId)
                .ToArray()
                .Contains(r.RoleOfferId)))
                .ToList();
            List<Volunteer> volunteers =new ();
            foreach (AssignOrWaitlistVolunteerDto volunteerDto in volunteerDtos)
            {
                Volunteer? updatedVolunteer = dbVolunteers
                    .FirstOrDefault(r => r.CandidateId == volunteerDto.Id);
                RoleOffer? roleOffer = dbRoleOffers
                    .FirstOrDefault(r => r.RoleOfferId == volunteerDto.RoleOfferId);

                // Check if the volunteer and role offer exist
                if (updatedVolunteer == null || roleOffer == null)
                {
                    return _jsonFactory
                        .CreateJson(StatusCodes.Status404NotFound,
                        "Volunteer or RoleOffer was not found");
                }
                updatedVolunteer.RoleOfferId = roleOffer.RoleOfferId;
                updatedVolunteer.Status = volunteerDto.Status;

                //Write History
                _historyServices.WriteHistory(updatedVolunteer,email);
              
                volunteers.Add(updatedVolunteer);
            }
            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);

            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK); ;
        }
        
        public async Task<ObjectResult> ChangeToAnyStatusAsync
            (ICollection<VolunteerChangeToAnyStatusDto> volunteerDtos, string email)
        {
            if (volunteerDtos.Count == 0)
                return _jsonFactory.CreateJson(StatusCodes.Status204NoContent);

            // Check if all statuses are valid
            if (volunteerDtos
                .Any(v => v.Status != null
                && v.Status.ToLower() != StatusConstants.PreAssigned.ToLower()
                && v.Status.ToLower() != StatusConstants.WaitlistOffered.ToLower()))
            {
                return _jsonFactory.CreateJson(StatusCodes.Status400BadRequest
                    , "Invalid Status");
            }

            ICollection<Volunteer> dbVolunteers = (await _unitOfWork.VolunteerRepository
               .GetAllAsNoTrackingAsync(v => !v.IsDeleted && volunteerDtos
               .Select(d => d.Id)
               .ToArray()
               .Contains(v.CandidateId)))
               .ToList();

            int[] volunteerRoleOfferIds = dbVolunteers
                .Where(v => v.RoleOfferId != null)
                .Select(v => (int) v.RoleOfferId)
                .ToArray();

            ICollection<RoleOffer> dbRoleOffers = (await _unitOfWork.RoleOfferRepository
                .GetAllSpecificRoleOffersAsNoTrackingAsync
                (r => !r.IsDeleted && volunteerRoleOfferIds.Contains(r.RoleOfferId)))
                .ToList();
            List<Volunteer> volunteers = new ();

            foreach (VolunteerChangeToAnyStatusDto volunteerDto in volunteerDtos)
            {
                Volunteer? updatedVolunteer = dbVolunteers
                    .FirstOrDefault(r=>r.CandidateId == volunteerDto.Id);

                if (updatedVolunteer == null)
                {
                    return _jsonFactory.CreateJson
                        (StatusCodes.Status404NotFound, "Volunteer is not found");
                }

                updatedVolunteer.Status = volunteerDto.Status;

                if (volunteerDto.Status == null)
                {
                    updatedVolunteer.RoleOfferId = null;
                }
                else
                {
                    if (updatedVolunteer.RoleOfferId != null) 
                    {
                        RoleOffer? roleOffer = dbRoleOffers
                            .FirstOrDefault(r => r.Id == (int)updatedVolunteer.RoleOfferId);
                        // Check if the role offer exists
                        if (roleOffer == null)
                        {
                            return _jsonFactory
                                .CreateJson(StatusCodes.Status404NotFound, 
                                "RoleOffer is not found");
                        }
                        updatedVolunteer.RoleOfferId = roleOffer.RoleOfferId;
                    }
                    else // if there is status but not RoleOffer
                    {
                        return _jsonFactory
                                .CreateJson(StatusCodes.Status400BadRequest,
                                "To set status, you need to include RoleOfferId");
                    }
                }
                //Write History
                _historyServices.WriteHistory(updatedVolunteer,email);

                volunteers.Add(updatedVolunteer);
            }

            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);
            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
