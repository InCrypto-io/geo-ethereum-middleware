using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class VerifyResultDto: BaseResponse
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}