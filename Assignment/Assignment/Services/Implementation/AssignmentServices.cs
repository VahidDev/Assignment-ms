using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using DomainModels.Models.Enums;
using Repository.RepositoryServices.Abstraction;
namespace Assignment.Services.Implementation
{
    public class AssignmentServices : IAssignmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AssignmentServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper=mapper;
        }
        public async Task<bool> AssignOrWaitlistAsync(ICollection<VolunteerDto> volunteerDtos)
        {
            List<Volunteer> volunteers =new ();
            foreach (VolunteerDto volunteerDto in volunteerDtos)
            {
                // Check if the volunteer and role offer exists
                if (!await _unitOfWork.VolunteerRepository
                    .AnyAsync(v => v.Id == volunteerDto.Id) 
                    || !await _unitOfWork.RoleOfferRepository
                    .AnyAsync(r => r.Id == volunteerDto.RoleOfferId)) 
                    return false;

                Volunteer updatedVolunteer =_mapper.Map<Volunteer>(volunteerDto);
                updatedVolunteer.UpdatedAt = DateTime.Now;
                volunteers.Add(updatedVolunteer);
            }
            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);

            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> ChangeToAnyStatusAsync(ICollection<VolunteerDto> volunteerDtos)
        {
            List<Volunteer> volunteers = new ();

            foreach (VolunteerDto volunteerDto in volunteerDtos)
            {
                Volunteer dbVolunteer = await _unitOfWork.VolunteerRepository
                       .GetByIdAsNoTrackingAsync(volunteerDto.Id);

                if (dbVolunteer == null || dbVolunteer.RoleOfferId== null) return false;
                // Check if the role offer exists
                if (!await _unitOfWork.RoleOfferRepository
                    .AnyAsync(r => r.Id == (int)dbVolunteer.RoleOfferId))
                    return false;

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
            return true;
        }
    }
}
