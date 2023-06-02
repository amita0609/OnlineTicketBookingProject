
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;

namespace OnlineTicketWeb.Services.IServices
{
    public interface IEventService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(EventDTO obj);
        Task<T> UpdateAsync<T>(Event obj);
        Task<T> DeleteAsync<T>(int id);
    }
}