using Microsoft.AspNetCore.Mvc;
using OnlineTicketData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using OnlineTicketData.Models;
using OnlineTicketData.Models.ViewModels;
using OnlineTicketWeb.Services.IServices;
using Newtonsoft.Json;
using OnlineTicketData.Repository.IRepository;

namespace OnlineTicketWeb.ViewComponents
{
   
    public class CountdownViewComponent : ViewComponent
    {
        private readonly IEventRepository _eventService;

        public CountdownViewComponent(IEventRepository eventService)
        {
            _eventService = eventService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            List<Event> list = await _eventService.GetAllAsync();
           
          
            
            var today = DateTime.Today;

            var firstEvent = list
                .Where(e => e.EventDate == today)
                .OrderBy(e => e.EventDate)
                .FirstOrDefault();

            if (firstEvent != null)
            {
                var countdownTime = firstEvent.EventDate - DateTime.Now;

                var model = new CountdownViewModel
                {
                    CountdownTime = countdownTime
                };

                return View(model);
            }

            // No event found for today
            return Content("No events today.");
        }
    }
}

