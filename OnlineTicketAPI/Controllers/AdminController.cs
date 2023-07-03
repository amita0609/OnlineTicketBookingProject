using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTicketData.Models;
using OnlineTicketData.Repository.IRepository;

namespace OnlineTicketAPI.Controllers
{
    [Route("api/Admin/[action]")]
    [ApiController]
    public class AdminController : Controller
    {


        private readonly IEventRepository _eventRepository;
        public AdminController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;

        }


        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            IEnumerable<Event> c =await _eventRepository.GetAllAsync(b => b.IsApproved == false);
            if (c == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(c);
            }

        }


        [HttpPut]
        public async Task<IActionResult> EventApproved(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Event ev =await  _eventRepository.GetAsync(b => b.EventId == id);
            if (ev == null)
            {
                return NotFound();

            }
            ev.IsApproved = true;
            _eventRepository.SaveAsync();
            return Ok(ev);
        }


        [HttpPut]
        public async Task<IActionResult> EventRejected(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Event c =await _eventRepository.GetAsync(b => b.EventId == id);
            if (c == null)
            {
                return NotFound();

            }
            c.IsApproved = false;
            _eventRepository.SaveAsync();
            return Ok(c);
        }

        [HttpPut]
        public async Task<IActionResult> PendingApproval()
        {
            var pendingEvents = await _eventRepository.GetAsync(e => !e.IsApproved);
            return Ok(pendingEvents);
        }
    }
}
