using DomainModels.Models.Entities;

namespace Assignment.Services.Abstraction
{
    public interface IHistoryServices
    {
        bool WriteHistory(Volunteer volunteer);
    }
}
