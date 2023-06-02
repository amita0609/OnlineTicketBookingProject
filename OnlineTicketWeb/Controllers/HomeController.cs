using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineTicketData.Models;
using OnlineTicketWeb.Services.IServices;
using System.Diagnostics;

namespace OnlineTicketWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public HomeController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Event> list = new();

            var response = await _eventService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
       
    }
}