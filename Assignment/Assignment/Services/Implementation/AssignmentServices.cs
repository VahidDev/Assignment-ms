using Assignment.Services.Abstraction;
using Assignment.Utilities.ServicesUtilities.AssignmentUtilities.Abstraction;
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
        private readonly IAssignmentUtilities _assignmentUtilities;
        public AssignmentServices(IUnitOfWork unitOfWork, IAssignmentUtilities assignmentUtilities)
        {
            _unitOfWork = unitOfWork;
            _assignmentUtilities = assignmentUtilities;
        }
        public async Task<bool> AssignOrWaitlistAsync(ICollection<VolunteerDto> volunteerDtos)
        {
            List<Volunteer> volunteers =new ();
            List<Entities.Assignment>  assignments =new ();
            foreach (VolunteerDto volunteerDto in volunteerDtos)
            {
                Volunteer dbVolunteer = await _unitOfWork.VolunteerRepository
                        .FindByIdAsync(volunteerDto.Id);
                RoleOffer dbRoleOffer = await _unitOfWork.RoleOfferRepository
                    .FindByIdAsync(volunteerDto.RoleOfferId);

                if (dbVolunteer == null || dbRoleOffer == null) return false;

                dbVolunteer.Status = volunteerDto.Status;
                dbVolunteer.RoleOfferId = volunteerDto.RoleOfferId;

                if (volunteerDto.Status == Statusenum.Assigned)
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
            List<Volunteer> volunteers = new ();
            List<Entities.Assignment>addedAssignments = new ();
            List<int> deletedAssignmentIds = new();

            foreach (VolunteerDto volunteerDto in volunteerDtos)
            {
                Volunteer dbVolunteer = await _unitOfWork.VolunteerRepository
                       .FindByIdAsync(volunteerDto.Id);

                if (dbVolunteer == null || dbVolunteer.RoleOfferId== null) return false;

                RoleOffer dbRoleOffer = await _unitOfWork.RoleOfferRepository
                    .FindByIdAsync((int)dbVolunteer.RoleOfferId);

                if (dbRoleOffer == null) return false;

                if (volunteerDto.Status == Statusenum.Assigned)
                {
                    if(dbVolunteer.Status!= Statusenum.Assigned)
                        addedAssignments.Add(new Entities.Assignment 
                        { VolunteerId = volunteerDto.Id, RoleOffer = dbRoleOffer });
                }
                else if(volunteerDto.Status == Statusenum.Waitlisted)
                {
                    if (!await _assignmentUtilities
                        .AddDeletingAssignmentIdToListAsync(deletedAssignmentIds, volunteerDto))
                        return false; 
                }
                else if(volunteerDto.Status == Statusenum.Free)
                {
                    dbVolunteer.RoleOfferId = null;
                    if (dbVolunteer.Status == Statusenum.Assigned)
                    {
                        if (!await _assignmentUtilities
                            .AddDeletingAssignmentIdToListAsync(deletedAssignmentIds, volunteerDto))
                            return false;
                    }
                }
                dbVolunteer.UpdatedAt = DateTime.Now;
                dbVolunteer.Status = volunteerDto.Status;
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
