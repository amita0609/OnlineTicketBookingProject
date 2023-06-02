using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
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

        public TicketBookingAPIController(ITicketBookingRepository dbTicketBooking, IEventRepository dbEvent, IMapper mapper)
        {
            _dbEvent = dbEvent;
            _dbTicketBooking = dbTicketBooking;
            _mapper = mapper;
            this._response = new();
        }

        //get

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllTicketBooking()
        {
            try
            {
                IEnumerable<TicketBooking> TicketBookings = await _dbTicketBooking.GetAllAsync(includeProperties:"Event");
                _response.Result = _mapper.Map<IEnumerable<TicketBookingDTO>>(TicketBookings);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        //getById
        [HttpGet("{id:int}", Name = "GetTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTicketBookingById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var obj = await _dbTicketBooking.GetAsync(u => u.TicketId == id);
                if (obj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<TicketBookingDTO>(obj);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }


        //post
        [HttpPost]
  
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CreateTicketBookingAsync([FromBody] TicketBookingDTO obj)
        {
            try
            {

               
                TicketBooking ticket = _mapper.Map<TicketBooking>(obj);
                await _dbTicketBooking.CreateAsync(ticket);
            
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetTicket", new { id = ticket.TicketId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //delete
      
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteTicketBooking")]
        public async Task<ActionResult<APIResponse>> DeleteTicketBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var user = await _dbTicketBooking.GetAsync(u => u.TicketId == id);
          
                await _dbTicketBooking.RemoveAsync(user);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateTicketBooking")]
        public async Task<ActionResult<APIResponse>> UpdateTicketBooking(int id, [FromBody] TicketBookingDTO obj)
        {
            try
            {
                TicketBooking ticket = _mapper.Map<TicketBooking>(obj);
                await _dbTicketBooking.UpdateAsync(ticket);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }



    }
}
