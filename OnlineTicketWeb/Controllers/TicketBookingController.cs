﻿using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            // var events=await _eventService.GetAllAsync<APIResponse>();
            // ViewBag.events = events;


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
        [ValidateAntiForgeryToken]
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

     
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _ticketBookingService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                Event model = JsonConvert.DeserializeObject<Event>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(TicketBooking model)
        {

            var response = await _ticketBookingService.DeleteAsync<APIResponse>(model.TicketId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Ticket deleted successfully";
                return RedirectToAction(nameof(IndexTicketBooking));
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }


        public async Task<IActionResult> ViewBookedTickets()
        {
            List<TicketBooking> list = new();

            var response = await _ticketBookingService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<TicketBooking>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }

}


