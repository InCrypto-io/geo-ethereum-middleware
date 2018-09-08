using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models.ChannelInfo
{
    
    [FunctionOutput]
    public class ChannelDetails
    {
        [Parameter("address", "alice", 1)]
        public string alice { get; set; }
        
        [Parameter("address", "bob", 2)]
        public string bob { get; set; }
        
        [Parameter("uint8", "state", 3)]
        public int state { get; set; }
        
        [Parameter("uint256", "balanceAlice", 4)]
        public BigInteger balanceAlice { get; set; }
        
        [Parameter("uint256", "balanceBob", 5)]
        public BigInteger balanceBob { get; set; }
    }
   
    public class ChannelDetailsJson
    {
        public string alice { get; set; }
        
        public string bob { get; set; }
        
        public int state { get; set; }
        
        public string balanceAlice { get; set; }
        
        public string balanceBob { get; set; }

        public static ChannelDetailsJson FromEhterModel(ChannelDetails details)
        {
            return new ChannelDetailsJson
            {
                alice = details.alice,
                bob = details.bob,
                state = details.state,
                balanceAlice = details.balanceAlice.ToString(),
                balanceBob = details.balanceBob.ToString()
            };
        }
    }
}