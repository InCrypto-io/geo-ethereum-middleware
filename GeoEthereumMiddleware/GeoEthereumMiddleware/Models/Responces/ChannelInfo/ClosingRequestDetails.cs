using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models.ChannelInfo
{
    [FunctionOutput]
    public class ClosingRequestDetails
    {

        [Parameter("uint256", "closingRequested", 1)]
        public uint closingRequested { get; set; }
        
        [Parameter("uint256", "channelEpoch", 2)]
        public uint channelEpoch { get; set; }
        
        [Parameter("uint128", "aliceNonce", 3)]
        public uint aliceNonce { get; set; }
        
        [Parameter("uint128", "bobNonce", 4)]
        public uint bobNonce { get; set; }
    }
}