using OnlineTicketData.Models;

namespace OnlineTicketWeb.Services.IServices
{
    public interface IAdminService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<bool> ApprovedEvent(int id);
        Task<bool> RejectEvent(int id);

        Task<IEnumerable<Event>> PendingApproval();


    }
}
