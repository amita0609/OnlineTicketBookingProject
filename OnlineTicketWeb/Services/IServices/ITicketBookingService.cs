using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;

namespace OnlineTicketWeb.Services.IServices
{
    public interface ITicketBookingService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(TicketBookingDTO obj);
        Task<T> UpdateAsync<T>(TicketBooking obj);
        Task<T> DeleteAsync<T>(int id);
    }
}