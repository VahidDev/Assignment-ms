using Assignment.Constants.VolunteerConstants;
using Assignment.Factory;
using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    internal class DashboardServices : IDashboardServices
    {
        private readonly IJsonFactory _jsonFactory;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardServices(IJsonFactory jsonFactory, IUnitOfWork unitOfWork)
        {
            _jsonFactory = jsonFactory;
            _unitOfWork = unitOfWork;
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
                OverallWaitlistDemand = countDtos.Sum(r=>r.WaitlistCount)
            };

            #region Getting all info
            int acceptedNumber = dbVolunteers
                .Where(v => v.Status != null)
                .DistinctBy(r => r.Status.ToLower() == StatusConstants.Accepted.ToLower())
                .Count();
            int assignedNumber = dbVolunteers
                .Where(v => v.Status != null)
                .DistinctBy(r => r.Status.ToLower() == StatusConstants.Assigned.ToLower())
                .Count();
            int preAssigned = (dbVolunteers
                .Where(v => v.Status != null)
                .DistinctBy(r => r.Status.ToLower() == StatusConstants.PreAssigned.ToLower())
                .Count());
            int pending = (dbVolunteers
                .Where(v => v.Status != null)
                .DistinctBy(r => r.Status.ToLower() == StatusConstants.Pending.ToLower())
                .Count());

            int waitlistAccepted = (dbVolunteers
              .Where(v => v.Status != null)
              .DistinctBy(r => r.Status.ToLower() == StatusConstants.WaitlistAccepted.ToLower())
              .Count());
            int waitlistAssigned = (dbVolunteers
             .Where(v => v.Status != null)
             .DistinctBy(r => r.Status.ToLower() == StatusConstants.WaitlistAssigned.ToLower())
             .Count());
            int waitlistOffered = (dbVolunteers
             .Where(v => v.Status != null)
             .DistinctBy(r => r.Status.ToLower() == StatusConstants.WaitlistOffered.ToLower())
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

            dto.OverallNoneAssigned = dto.OverallAssigneeDemand -
                (assignedNumber + acceptedNumber + pending + preAssigned);
            dto.OverallNoneWaitlisted = dto.OverallAssigneeDemand -
                (waitlistOffered + waitlistAssigned + waitlistAccepted);

            dto.TotalAssigned 
                = dto.Accepted + dto.Assigned + dto.Pending + dto.PreAssigned;
            dto.TotalWaitlisted
                = dto.WaitlistOffered + dto.WaitlistAccepted + dto.WaitlistAssigned;

            return _jsonFactory.CreateJson(StatusCodes.Status200OK, null, dto);
        }
    }
}
