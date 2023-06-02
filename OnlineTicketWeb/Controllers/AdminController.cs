using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTicketData.StaticData;
using OnlineTicketWeb.Services.IServices;
using System.Data;

namespace OnlineTicketWeb.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminController:Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _adminService.GetUsersAsync();
            return View(posts);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var eventToApprove = _adminService.ApprovedEvent(id);
           

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
           
                await _adminService.RejectEvent(id);
                return RedirectToAction("Index");
           
           
        }

        //public async Task<IActionResult> PendingApproval()
        //{
        //    var pendingEvents =await _eventService.GetAsync(e => !e.IsApproved).ToList();
        //    return View(pendingEvents);
        //}
    }
}
    

