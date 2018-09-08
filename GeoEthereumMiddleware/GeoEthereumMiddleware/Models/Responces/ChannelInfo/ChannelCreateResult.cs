using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models.ChannelInfo
{
    public class ChannelCreateResult: TransactionResultDto
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
    }
}