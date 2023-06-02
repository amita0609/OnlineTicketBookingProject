using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineTicketData.Models;
using OnlineTicketWeb.Services.IServices;
using System.Collections.Generic;
using System.Net;

namespace OnlineTicketWeb.Services
{
    public class AdminService : IAdminService
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:5001";

        public AdminService(APIResponse response, IMapper mapper,HttpClient httpClient, IConfiguration configuration)
        {
             _response = response;
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
           
        }
        public async Task<IEnumerable<Event>> GetUsersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/AdminController/GetAllEvents");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<Event> evs = JsonConvert.DeserializeObject<IEnumerable<Event>>(content);
            return evs;
        }

        public async Task<bool> ApprovedEvent(int id)
        {
            var response = await _httpClient.GetAsync($"api/AdminController/EventApproved?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RejectEvent(int id)
        {
            var response = await _httpClient.GetAsync($"api/AdminController/EventRejected?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public Task<IEnumerable<Event>> PendingApproval()
        {
            throw new NotImplementedException();
        }


        //public async Task<IEnumerable<Event>> PendingApproval()
        //{
        //    var list = await _httpClient.GetAsync($"api/AdminController/PendingApproval");
        //    _response.Result = _mapper.Map<IEnumerable<Event>>(list);
        //    _response.StatusCode = HttpStatusCode.OK;
     //   IEnumerable<Event> evs = JsonConvert.DeserializeObject<IEnumerable<Event>>(list);
        //    return _response;
        //}


    }
}
