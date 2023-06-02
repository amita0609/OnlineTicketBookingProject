using Microsoft.EntityFrameworkCore;
using OnlineTicketData.Db;
using OnlineTicketData.Models;
using OnlineTicketData.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
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



        public async Task<TicketBooking> UpdateAsync(TicketBooking entity)
        {

            _db.TicketBookings.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }




    }
}