using Assignment.Factory;
using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using DomainModels.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;
namespace Assignment.Services.Implementation
{
    internal class AssignmentServices : IAssignmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJsonFactory _jsonFactory;
        public AssignmentServices(IUnitOfWork unitOfWork,IMapper mapper
            , IJsonFactory jsonFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper=mapper;
            _jsonFactory=jsonFactory;
        }
        public async Task<JsonResult> AssignOrWaitlistAsync
            (ICollection<AssignOrWaitlistVolunteerDto> volunteerDtos)
        {
            if (volunteerDtos.Count == 0)
                return _jsonFactory.CreateJson(StatusCodes.Status204NoContent);

            List<Volunteer> volunteers =new ();
            foreach (AssignOrWaitlistVolunteerDto volunteerDto in volunteerDtos)
            {
                // Check if the volunteer and role offer exist
                if (!await _unitOfWork.VolunteerRepository
                    .AnyAsync(v => v.Id == volunteerDto.Id) 
                    || !await _unitOfWork.RoleOfferRepository
                    .AnyAsync(r => r.Id == volunteerDto.RoleOfferId)) 
                    return _jsonFactory.CreateJson(StatusCodes.Status404NotFound
                        ,"Volunteer or RoleOffer was not found"); 

                Volunteer updatedVolunteer =_mapper.Map<Volunteer>(volunteerDto);
                updatedVolunteer.UpdatedAt = DateTime.Now;
                volunteers.Add(updatedVolunteer);
            }
            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);

            await _unitOfWork.CompleteAsync();
            return _jsonFactory.CreateJson(StatusCodes.Status200OK); ;
        }
        public async Task<JsonResult> ChangeToAnyStatusAsync
            (ICollection<VolunteerChangeToAnyStatusDto> volunteerDtos)
        {
            if (volunteerDtos.Count == 0)
                return _jsonFactory.CreateJson(StatusCodes.Status204NoContent);

            List<Volunteer> volunteers = new ();

            foreach (VolunteerChangeToAnyStatusDto volunteerDto in volunteerDtos)
            {
                Volunteer dbVolunteer = await _unitOfWork.VolunteerRepository
                       .GetByIdAsNoTrackingAsync(volunteerDto.Id??0);

                if (dbVolunteer == null || dbVolunteer.RoleOfferId== null)
                    return _jsonFactory.CreateJson
                        (StatusCodes.Status404NotFound,"Volunteer is not found"); 
                // Check if the role offer exists
                if (!await _unitOfWork.RoleOfferRepository
                    .AnyAsync(r => r.Id == (int)dbVolunteer.RoleOfferId))
                    return _jsonFactory
                        .CreateJson(StatusCodes.Status404NotFound, "RoleOffer is not found");

                Volunteer updatedVolunteer = _mapper.Map<Volunteer>(volunteerDto);
                updatedVolunteer.RoleOfferId = dbVolunteer.RoleOfferId;
                updatedVolunteer.UpdatedAt = DateTime.Now;

                if (volunteerDto.Status == Statusenum.Free)
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
