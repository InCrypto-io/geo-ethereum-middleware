using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class AuditSignDto: CooperativeSignDto
    {
        [JsonProperty("epoch_id")]
        public uint EpochId { get; set; }
    }
}