using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class ChannelCloseCooperativeDto
    {
        
        [JsonProperty("bob_amount")]
        public string BobAmountWei { get; set; }

        [JsonProperty("alice_amount")]
        public string AliceAmountWei { get; set; }
        
        [JsonProperty("bob_signature")]
        public string BobSignature { get; set; }

        [JsonProperty("alice_signature")]
        public string AliceSignature { get; set; }
        
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
    }
}