using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class CooperativeVerifyDto:CooperativeSignDto
    {
        [JsonProperty("address_to_verify")]
        public string Address { get; set; }
        
        [JsonProperty("signature_to_verify")]
        public string Signature { get; set; }
    }
}