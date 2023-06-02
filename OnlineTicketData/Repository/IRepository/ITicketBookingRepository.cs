using OnlineTicketData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTicketData.Repository.IRepository
{
    
    public interface ITicketBookingRepository : IRepository<TicketBooking>
    {
        Task<TicketBooking> UpdateAsync(TicketBooking entity);


    }
}
