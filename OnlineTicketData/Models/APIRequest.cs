using static OnlineTicketData.StaticData.SD;

namespace OnlineTicketData.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
      
    }
}
