using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class CreateChannelDto
    {
        [JsonProperty("amount")]
        public string AmountWei { get; set; }

        [JsonProperty("receiver_address")]
        public string ReceiverAddress { get; set; }
    }
}