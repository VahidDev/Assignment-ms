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
        public AssignmentServices(IUnitOfWork unitOfWork,IMapper mapper, 
            IJsonFactory jsonFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper=mapper;
            _jsonFactory=jsonFactory;
        }
        public async Task<ObjectResult> AssignOrWaitlistAsync
            (ICollection<AssignOrWaitlistVolunteerDto> volunteerDtos)
        {
            if (volunteerDtos.Count == 0)
                return _jsonFactory.CreateJson(StatusCodes.Status204NoContent);

            ICollection<Volunteer> dbVolunteers = (await _unitOfWork.VolunteerRepository
                .GetAllAsNoTrackingAsync(v=> !v.IsDeleted && volunteerDtos.Select(d=>d.CandidateId).ToArray()
                .Contains(v.CandidateId))).ToList();
            ICollection<RoleOffer>dbRoleOffers = (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingAsync(r=>!r.IsDeleted 
                && volunteerDtos.Select(d=>d.RoleOfferId).ToArray().Contains(r.Id))).ToList();
            List<Volunteer> volunteers =new ();
            foreach (AssignOrWaitlistVolunteerDto volunteerDto in volunteerDtos)
            {
                // Check if the volunteer and role offer exist
                if (!dbVolunteers.Any(v => v.CandidateId == volunteerDto.CandidateId) 
                    || !dbRoleOffers.Any(r => r.Id == volunteerDto.RoleOfferId)) 
                    return _jsonFactory.CreateJson(StatusCodes.Status404NotFound,
                        "Volunteer or RoleOffer was not found"); 

                Volunteer updatedVolunteer =_mapper.Map<Volunteer>(volunteerDto);
                
                volunteers.Add(updatedVolunteer);
            }
            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);

            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK); ;
        }
        public async Task<ObjectResult> ChangeToAnyStatusAsync
            (ICollection<VolunteerChangeToAnyStatusDto> volunteerDtos)
        {
            if (volunteerDtos.Count == 0)
                return _jsonFactory.CreateJson(StatusCodes.Status204NoContent);

            ICollection<Volunteer> dbVolunteers = (await _unitOfWork.VolunteerRepository
               .GetAllAsNoTrackingAsync(v => !v.IsDeleted && volunteerDtos.Select(d => d.CandidateId).ToArray()
               .Contains(v.CandidateId))).ToList();
            ICollection<RoleOffer> dbRoleOffers = (await _unitOfWork.RoleOfferRepository
                .GetAllAsNoTrackingAsync(r => !r.IsDeleted)).ToList();
            List<Volunteer> volunteers = new ();

            foreach (VolunteerChangeToAnyStatusDto volunteerDto in volunteerDtos)
            {
                Volunteer? dbVolunteer = dbVolunteers.FirstOrDefault(r=>r.CandidateId == volunteerDto.CandidateId);

                if (dbVolunteer == null || dbVolunteer.RoleOfferId== null)
                    return _jsonFactory.CreateJson
                        (StatusCodes.Status404NotFound,"Volunteer is not found"); 
                // Check if the role offer exists
                if (!dbRoleOffers.Any(r => r.Id == (int)dbVolunteer.RoleOfferId))
                    return _jsonFactory
                        .CreateJson(StatusCodes.Status404NotFound, "RoleOffer is not found");

                Volunteer updatedVolunteer = _mapper.Map<Volunteer>(volunteerDto);
                updatedVolunteer.RoleOfferId = dbVolunteer.RoleOfferId;

                if (volunteerDto.Status == null)
                {
                    updatedVolunteer.RoleOfferId = null;
                }
                volunteers.Add(updatedVolunteer);
            }
            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);

            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK);
        }
    }
}
