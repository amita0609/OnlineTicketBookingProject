using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineTicketData.Models;
using OnlineTicketWeb.Services.IServices;
using System.Collections.Generic;
using System.Data;

namespace OnlineTicketWeb.Controllers
{
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


     //   [Authorize(Roles = "admin")]
    //    public async Task<IActionResult> CreateTicketBooking()
    //    {
          
           
    //           IEnumerable<SelectListItem>events= _eventService.GetAllAsync()
    //            .Select(i => new SelectListItem
    //                {
    //                    Text = i.Name,
    //                    Value = i.Id.ToString()
    //                });
    //          ViewBag.Events = Events;
    //        }
    //         return View();
    //    }


    //    [Authorize(Roles = "admin")]
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> CreateTicketBooking(TicketBookingCreateVM model)
    //    {
    //        if (ModelState.IsValid)
    //        {

    //            var response = await _ticketBookingService.CreateAsync<APIResponse>(model.TicketBooking, HttpContext.Session.GetString(SD.SessionToken));
    //            if (response != null && response.IsSuccess)
    //            {
    //                return RedirectToAction(nameof(IndexTicketBooking));
    //            }
    //            else
    //            {
    //                if (response.ErrorMessages.Count > 0)
    //                {
    //                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
    //                }
    //            }
    //        }

    //        var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
    //        if (resp != null && resp.IsSuccess)
    //        {
    //            model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
    //                (Convert.ToString(resp.Result)).Select(i => new SelectListItem
    //                {
    //                    Text = i.Name,
    //                    Value = i.Id.ToString()
    //                }); ;
    //        }
    //        return View(model);
    //    }


    //    [Authorize(Roles = "admin")]
    //    public async Task<IActionResult> UpdateTicketBooking(int villaNo)
    //    {
    //        TicketBookingUpdateVM villaNumberVM = new();
    //        var response = await _ticketBookingService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(SD.SessionToken));
    //        if (response != null && response.IsSuccess)
    //        {
    //            TicketBookingDTO model = JsonConvert.DeserializeObject<TicketBookingDTO>(Convert.ToString(response.Result));
    //            villaNumberVM.TicketBooking = _mapper.Map<TicketBookingUpdateDTO>(model);
    //        }

    //        response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
    //        if (response != null && response.IsSuccess)
    //        {
    //            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
    //                (Convert.ToString(response.Result)).Select(i => new SelectListItem
    //                {
    //                    Text = i.Name,
    //                    Value = i.Id.ToString()
    //                });
    //            return View(villaNumberVM);
    //        }


    //        return NotFound();
    //    }


    //    [Authorize(Roles = "admin")]
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> UpdateTicketBooking(TicketBookingUpdateVM model)
    //    {
    //        if (ModelState.IsValid)
    //        {

    //            var response = await _ticketBookingService.UpdateAsync<APIResponse>(model.TicketBooking, HttpContext.Session.GetString(SD.SessionToken));
    //            if (response != null && response.IsSuccess)
    //            {
    //                return RedirectToAction(nameof(IndexTicketBooking));
    //            }
    //            else
    //            {
    //                if (response.ErrorMessages.Count > 0)
    //                {
    //                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
    //                }
    //            }
    //        }

    //        var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
    //        if (resp != null && resp.IsSuccess)
    //        {
    //            model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
    //                (Convert.ToString(resp.Result)).Select(i => new SelectListItem
    //                {
    //                    Text = i.Name,
    //                    Value = i.Id.ToString()
    //                }); ;
    //        }
    //        return View(model);
    //    }


    //    [Authorize(Roles = "admin")]
    //    public async Task<IActionResult> DeleteTicketBooking(int villaNo)
    //    {
    //        TicketBookingDeleteVM villaNumberVM = new();
    //        var response = await _ticketBookingService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(SD.SessionToken));
    //        if (response != null && response.IsSuccess)
    //        {
    //            TicketBookingDTO model = JsonConvert.DeserializeObject<TicketBookingDTO>(Convert.ToString(response.Result));
    //            villaNumberVM.TicketBooking = model;
    //        }

    //        response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
    //        if (response != null && response.IsSuccess)
    //        {
    //            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
    //                (Convert.ToString(response.Result)).Select(i => new SelectListItem
    //                {
    //                    Text = i.Name,
    //                    Value = i.Id.ToString()
    //                });
    //            return View(villaNumberVM);
    //        }


    //        return NotFound();
    //    }



    //    [Authorize(Roles = "admin")]
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteTicketBooking(TicketBookingDeleteVM model)
    //    {

    //        var response = await _ticketBookingService.DeleteAsync<APIResponse>(model.TicketBooking.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
    //        if (response != null && response.IsSuccess)
    //        {
    //            return RedirectToAction(nameof(IndexTicketBooking));
    //        }

    //        return View(model);
    //    }



   }
}