using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class SignatureResponse: BaseResponse
    {
        [JsonProperty("sender_signature")]
        public string SenderSignature { get; set; }
    }
}