using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using DomainModels.Models.Enums;
using Repository.RepositoryServices.Abstraction;
using Entities= DomainModels.Models.Entities;
namespace Assignment.Services.Implementation
{
    public class AssignmentServices : IAssignmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssignmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AssignOrWaitlistAsync(ICollection<VolunteerDto> volunteerDtos)
        {
            ICollection<Volunteer> volunteers=new List<Volunteer>();
            ICollection<Entities.Assignment> assignments=new List<Entities.Assignment>();
            foreach (VolunteerDto volunteerDto in volunteerDtos)
            {
                Volunteer dbVolunteer = await _unitOfWork.VolunteerRepository
                        .FindByIdAsync(volunteerDto.Id);
                RoleOffer dbRoleOffer = await _unitOfWork.RoleOfferRepository
                    .FindByIdAsync(volunteerDto.RoleOfferId);
                if (dbVolunteer == null || dbRoleOffer == null) return false;

                dbVolunteer.Status = volunteerDto.Status;
                dbVolunteer.RoleOfferId = volunteerDto.RoleOfferId;

                if (volunteerDto.Status == Status.Assigned)
                {
                    assignments.Add
                        (new Entities.Assignment {VolunteerId= volunteerDto.Id,RoleOffer =dbRoleOffer});
                }
                volunteers.Add(dbVolunteer);
            }
            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);

            if(assignments.Count > 0)
                await _unitOfWork.AssignmentRepository.AddRangeAsync(assignments);

            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> ChangeToAnyStatusAsync(ICollection<VolunteerDto> volunteerDtos)
        {
            ICollection<Volunteer> volunteers = new List<Volunteer>();
            ICollection<Entities.Assignment> addedAssignments = new List<Entities.Assignment>();
            ICollection<int> deletedAssignmentIds = new List<int>();

            foreach (VolunteerDto volunteerDto in volunteerDtos)
            {
                Volunteer dbVolunteer = await _unitOfWork.VolunteerRepository
                       .FindByIdAsync(volunteerDto.Id);

                if (dbVolunteer == null || dbVolunteer.RoleOfferId== null) return false;

                RoleOffer dbRoleOffer = await _unitOfWork.RoleOfferRepository
                    .FindByIdAsync((int)dbVolunteer.RoleOfferId);

                if (dbRoleOffer == null) return false;

                dbVolunteer.Status = volunteerDto.Status;

                if (volunteerDto.Status == Status.Assigned)
                {
                    addedAssignments.Add
                       (new Entities.Assignment { VolunteerId = volunteerDto.Id, RoleOffer = dbRoleOffer });
                }
                else if(volunteerDto.Status == Status.Free)
                {
                    dbVolunteer.RoleOfferId = null;
                    deletedAssignmentIds.Add((await _unitOfWork.AssignmentRepository
                        .FindByIdAsync(volunteerDto.Id)).Id);
                }
                volunteers.Add(dbVolunteer);
            }

            _unitOfWork.VolunteerRepository.UpdateRange(volunteers);
            if (addedAssignments.Count > 0)
                await _unitOfWork.AssignmentRepository.AddRangeAsync(addedAssignments);

            if (deletedAssignmentIds.Count > 0)
                await _unitOfWork.AssignmentRepository.DeleteRangeAsync(deletedAssignmentIds);

            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
