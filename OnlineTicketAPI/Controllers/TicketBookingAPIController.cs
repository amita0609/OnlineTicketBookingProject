using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineTicketData.Db;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.Repository;
using OnlineTicketData.Repository.IRepository;
using System.Data;
using System.Net;

namespace OnlineTicketAPI.Controllers
{
    [Route("api/TicketBookingAPI")]
    [ApiController]
    // [Authorize]
    public class TicketBookingAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ITicketBookingRepository _dbTicketBooking;
        private readonly IEventRepository _dbEvent;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;


        public TicketBookingAPIController(ApplicationDbContext context, ITicketBookingRepository dbTicketBooking, IEventRepository dbEvent, IMapper mapper)
        {
            _dbEvent = dbEvent;
            _dbTicketBooking = dbTicketBooking;
            _mapper = mapper;
            this._response = new();
            _context = context;
        }

        //get

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllTicketBooking()
        {

            IEnumerable<TicketBooking> TicketBookings = await _dbTicketBooking.GetAllAsync(includeProperties: "Event");

            return Ok(TicketBookings);

        }



        //getById
        [HttpGet("{id:int}", Name = "GetTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetTicketBookingById(int id)
        {


            var obj = _dbTicketBooking.GetAsync(u => u.TicketId == id, includeProperties: "Event");
            if (obj == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }





        //post
        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateTicketBooking([FromBody] TicketBookingDTO obj)
        {

            var EV = _context.Events.FirstOrDefault(u => u.EventId == obj.EvId);



            if (EV.AvailableSeats <= 0)
                return BadRequest("No available seats.");

            var booking = new TicketBookingDTO
            {

                EvId = obj.EvId,
                CustomerName = obj.CustomerName

            };
            TicketBooking b = _mapper.Map<TicketBooking>(obj);
            await _dbTicketBooking.CreateAsync(b);
            EV.AvailableSeats--;
            _dbEvent.UpdateAsync(EV);

            _dbEvent.SaveAsync();
            return Ok(booking);

        }

        ////delete

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}", Name = "DeleteTicketBooking")]
        public async Task<ActionResult> DeleteTicketBooking(int id)
        {



            var booking = await _dbTicketBooking.GetAsync(u => u.TicketId == id);
          

            await _dbTicketBooking.RemoveAsync(booking);

            var events = _context.Events.FirstOrDefault(u => u.EventId == booking.EvId);
            if (events != null)
            {
                events.AvailableSeats++;
              await  _dbEvent.UpdateAsync(events);
                _dbEvent.SaveAsync();
              
                return NoContent();
            }
            return BadRequest();

        }

        //update
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpPut("{id:int}", Name = "UpdateTicketBooking")]
        public async Task<ActionResult> UpdateTicketBooking(int id, [FromBody] TicketBookingDTO obj)
        {
            TicketBooking ticket = _mapper.Map<TicketBooking>(obj);
            await _dbTicketBooking.UpdateAsync(ticket);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }


    



   }
}

