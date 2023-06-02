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
    public class TicketBookingRepository : Repository<TicketBooking>, ITicketBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketBookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       

        public async Task<TicketBooking> UpdateAsync(TicketBooking obj)
        {
            _db.TicketBookings.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}