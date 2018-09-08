using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class TransactionResultDto: BaseResponse
    {
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
    }
}