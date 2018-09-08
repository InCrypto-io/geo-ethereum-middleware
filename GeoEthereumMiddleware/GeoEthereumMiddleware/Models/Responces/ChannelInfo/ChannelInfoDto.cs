using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models.ChannelInfo
{
    public class ChannelInfoDto
    {
        [JsonProperty("channel_details")]
        public ChannelDetailsJson ChannelDetails { get; set; }
        
        [JsonProperty("channel_request_details")]
        public ClosingRequestDetails ClosingRequestDetails { get; set; }
    }
}