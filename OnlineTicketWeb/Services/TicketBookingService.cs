
using Newtonsoft.Json.Linq;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.StaticData;
using OnlineTicketWeb.Services;
using OnlineTicketWeb.Services.IServices;

namespace OnlineTicketWeb.Services
{
    public class TicketBookingService : BaseService, ITicketBookingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string TicketBookingUrl;

        public TicketBookingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            TicketBookingUrl = configuration.GetValue<string>("ServiceUrls:TicketAPI");

        }

        public Task<T> CreateAsync<T>(TicketBookingDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = TicketBookingUrl + "/api/TicketBookingAPI",
             
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = TicketBookingUrl + "/api/TicketBookingAPI/" + id,
              
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = TicketBookingUrl + "/api/TicketBookingAPI",
                
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = TicketBookingUrl + "/api/TicketBookingAPI/" + id,
              
            });
        }

        public Task<T> UpdateAsync<T>(TicketBooking dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = TicketBookingUrl + "/api/TicketBookingAPI/" + dto.TicketId,
               
            });
        }
    }
}