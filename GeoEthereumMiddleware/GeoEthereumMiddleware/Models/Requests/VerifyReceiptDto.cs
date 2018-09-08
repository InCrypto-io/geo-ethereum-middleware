using System;
using Newtonsoft.Json;

namespace GeoEthereumMiddleware.Models
{
    public class VerifyReceiptDto : ReceiptDto
    {
        [JsonProperty("address_to_verify")]
        public string Address { get; set; }
       
        [JsonProperty("signature_to_verify")]
        public string Signature { get; set; }
    }
    
    public class ReceiptDto
    {
        [JsonProperty("receipt_id")]
        public uint ReceiptId { get; set; }
        
        [JsonProperty("epoch_id")]
        public uint EpochId { get; set; }
        
        [JsonProperty("amount")]
        public string AmountWei { get; set; }

        [JsonProperty("receiver_address")]
        public string Address { get; set; }
        
    }
}