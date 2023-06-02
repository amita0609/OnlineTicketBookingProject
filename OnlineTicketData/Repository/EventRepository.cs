using OnlineTicketData.Db;
using OnlineTicketData.Models;
using OnlineTicketData.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineTicketData.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _db;
        public EventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Event> UpdateAsync(Event obj)
        {
            _db.Events.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

       





    }
}