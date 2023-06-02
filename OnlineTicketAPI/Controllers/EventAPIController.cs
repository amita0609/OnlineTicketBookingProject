using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTicketData.Db;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.Repository.IRepository;
using System.Data;
using System.Net;

namespace OnlineTicketAPI.Controllers
{
    [Route("api/EventAPI")]
    [ApiController]
    [Authorize]
    public class EventAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IEventRepository _dbEvent;
        private readonly IMapper _mapper;

       

        public EventAPIController(IEventRepository dbEvent, IMapper mapper)
        {
            _dbEvent = dbEvent;
            _mapper = mapper;
            this._response = new();
          
        }

        //get

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllEvent()
        {
            try
            {

                IEnumerable<Event> Events = await _dbEvent.GetAllAsync();
             
                _response.Result = _mapper.Map<IEnumerable<Event>>(Events);
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
        [HttpGet("{id:int}", Name = "GetEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEventById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var obj = await _dbEvent.GetAsync(u => u.EventId == id);
                if (obj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<Event>(obj);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CreateEventAsync([FromBody] EventDTO obj)
        {
            try
            {
                Event events = _mapper.Map<Event>(obj);
                await _dbEvent.CreateAsync(events);
 
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetEvent", new { id = events.EventId }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteEvent")]
        public async Task<ActionResult<APIResponse>> DeleteEvent(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var user = await _dbEvent.GetAsync(u => u.EventId == id);
          
                await _dbEvent.RemoveAsync(user);

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
        [HttpPut("{id:int}", Name = "UpdateEvent")]
        public async Task<ActionResult<APIResponse>> UpdateEvent(int id, [FromBody] Event obj)
        {
            try
            {
    
                await _dbEvent.UpdateAsync(obj);
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
