using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.StaticData;
using OnlineTicketWeb.Services;
using OnlineTicketWeb.Services.IServices;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;

namespace OnlineTicketWeb.Controllers
{
    [Authorize]
    public class TicketBookingController : Controller
    {
        private readonly ITicketBookingService _ticketBookingService;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public TicketBookingController(ITicketBookingService ticketBookingService, IMapper mapper, IEventService eventService)
        {
            _ticketBookingService = ticketBookingService;
            _mapper = mapper;
            _eventService = eventService;
        }

        public async Task<IActionResult> IndexTicketBooking()
        {
            List<TicketBooking> list = new();

            var response = await _ticketBookingService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<TicketBooking>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        
        public async Task<IActionResult> BookTicket()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> BookTicket(TicketBookingDTO ticket)
        {
            var response = await _ticketBookingService.CreateAsync<APIResponse>(ticket);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Ticked Booked successfully";
                return RedirectToAction(nameof(IndexTicketBooking));
            }
        
             TempData["error"] = "Error encountered.";
           
            return View(ticket);
        }


    }

}
