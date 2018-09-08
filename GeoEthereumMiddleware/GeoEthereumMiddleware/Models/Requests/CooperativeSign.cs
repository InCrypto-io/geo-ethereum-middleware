using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class CooperativeSignDto
    {
        [JsonProperty("bob_amount")]
        public string BobAmountWei { get; set; }

        [JsonProperty("alice_amount")]
        public string AliceAmountWei { get; set; }
        
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
    }
}