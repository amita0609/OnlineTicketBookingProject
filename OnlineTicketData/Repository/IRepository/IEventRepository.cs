using OnlineTicketData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTicketData.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event> UpdateAsync(Event entity);


    }
}
