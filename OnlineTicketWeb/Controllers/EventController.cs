using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineTicketData.Db;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.StaticData;
using OnlineTicketWeb.Services.IServices;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OnlineTicketWeb.Controllers
{
    [Authorize(Roles =SD.Role_Admin)]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        
        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
            
        }

        public async Task<IActionResult> IndexEvent()
        {
            List<Event> list = new();

            var response = await _eventService.GetAllAsync<APIResponse>();
         
            if (response != null)
            {
              
                list = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

         // [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateEvent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(EventDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _eventService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Event created successfully";
                    return RedirectToAction(nameof(IndexEvent));
                }
            }
              TempData["error"] = "Error encountered.";
            return View(model);

        }

        //  [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEvent(int eventId)
        {
            var response = await _eventService.GetAsync<APIResponse>(eventId);
            if (response != null && response.IsSuccess)
            {

                Event model = JsonConvert.DeserializeObject<Event>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }



         // [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEvent(Event model)
        {
            if (ModelState.IsValid)
            {
               
                var response = await _eventService.UpdateAsync<APIResponse>(model);
               
                if (response != null && response.IsSuccess)
                {
                   TempData["success"] = "Event updated successfully";
                    return RedirectToAction(nameof(IndexEvent));
                }
            }
              TempData["error"] = "Error encountered.";
            return View(model);
        }


        ////  // [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var response = await _eventService.GetAsync<APIResponse>(eventId);
            if (response != null && response.IsSuccess)
            {
                Event model = JsonConvert.DeserializeObject<Event>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }



        //// //  [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(Event model)
        {

            var response = await _eventService.DeleteAsync<APIResponse>(model.EventId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Event deleted successfully";
                return RedirectToAction(nameof(IndexEvent));
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }






      

        
    }
}