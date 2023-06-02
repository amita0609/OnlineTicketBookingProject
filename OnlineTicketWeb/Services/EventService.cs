using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.StaticData;
using OnlineTicketWeb.Services;
using OnlineTicketWeb.Services.IServices;

namespace OnlineTicketWeb.Services
{
    public class EventService : BaseService, IEventService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string eventUrl;

        public EventService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            eventUrl = configuration.GetValue<string>("ServiceUrls:TicketAPI");

        }

        public Task<T> CreateAsync<T>(EventDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = eventUrl + "/api/EventAPI",
            });
        }

      

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = eventUrl + "/api/EventAPI/" + id,
              
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = eventUrl + "/api/EventAPI/",
               
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = eventUrl + "/api/EventAPI/" + id,
              
            });
        }

        public Task<T> UpdateAsync<T>(Event obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = obj,
                Url = eventUrl + "/api/EventAPI/" + obj.EventId,
               
            });
        }
    }
}