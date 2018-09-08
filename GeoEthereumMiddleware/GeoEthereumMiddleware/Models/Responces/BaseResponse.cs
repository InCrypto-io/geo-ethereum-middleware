using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class BaseResponse
    {
        [JsonIgnore]
        public StatusMessageEnum Status { get; set; }
        
        [JsonProperty("status")]
        public string StatusString => Status.ToString();
    }

    public enum StatusMessageEnum
    {
        OK,
        BAD
    }
}