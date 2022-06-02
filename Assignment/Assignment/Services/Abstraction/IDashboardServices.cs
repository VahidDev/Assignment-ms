using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IDashboardServices
    {
        Task<ObjectResult> GetAllInfoAsync(); 
        Task<ObjectResult> GetRoleOffersAsync(int[] roleOfferIds); 
    }
}
