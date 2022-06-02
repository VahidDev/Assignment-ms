using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IDashboardServices
    {
        Task<ObjectResult> GetAllInfoAsync(); 
        Task<ObjectResult> GetRoleOffersAsync(int[] roleOfferIds);
        Task<ObjectResult> GetVolunteersInfoAsync(RoleOfferVolunteerDto dto);
    }
}
