using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoEthereumMiddleware.Configuration;
using GeoEthereumMiddleware.EthereumDal;
using GeoEthereumMiddleware.Models;
using GeoEthereumMiddleware.Models.ChannelInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.Extensions.Options;

namespace GeoEthereumMiddleware.Controllers
{
    [Route("api/")]
    public class GeoMiddlewareApiController : Controller
    {

        private readonly EthereumConfig _config;
        private readonly ChannelsContract _contract;
        
        public GeoMiddlewareApiController(IOptions<EthereumConfig> config)
        {
            _config = config.Value;
            _contract = new ChannelsContract(_config);
        }
        #region Receipt

        [HttpPost]
        [Route("receipts")]
        public SignatureResponse Receipts([FromBody] ReceiptDto receipt)
        {
            var signature = EthereumUtils.SignReceipts(receipt, _config.PrivateKey);  
            
            return new SignatureResponse
            {
                SenderSignature = signature
            };
        }
        
        [HttpPost]
        [Route("receipts/verify_signature")]
        public VerifyResultDto VerifySignature([FromBody] VerifyReceiptDto verifyReceipt)
        {
            var result = EthereumUtils.VerifyReceipts(verifyReceipt);  
            
            return new VerifyResultDto
            {
                Result = result
            };
        }

        #endregion

        #region Simple Create/Close

        [HttpPost]
        [Route("channel/create")]
        public async Task<ChannelCreateResult> CreateChannel([FromBody] CreateChannelDto channel)
        {
            var addressSender = EthereumUtils.GetAddressFromPrivateKey(_config.PrivateKey);
            var transcationId = await _contract.CreateChannel(channel);
            return new ChannelCreateResult
            {
                TransactionId = transcationId,
                ChannelId = EthereumUtils.GetChannelAddress(addressSender, channel.ReceiverAddress)
            };
        }
        
        [HttpPost]
        [Route("channel/create/reply")]
        public async Task<ChannelCreateResult> CreateChannelReply([FromBody] CreateChannelDto channel)
        {
            var addressSender = EthereumUtils.GetAddressFromPrivateKey(_config.PrivateKey);
            var transcationId = await _contract.CreateChannelReply(channel);
            return new ChannelCreateResult
            {
                TransactionId = transcationId,
                ChannelId = EthereumUtils.GetChannelAddress(channel.ReceiverAddress, addressSender)
            };
        }
        
        [HttpPost]
        [Route("channel/close")]
        public TransactionResultDto CloseChannel([FromBody] string channelId)
        {
            return new TransactionResultDto
            {
                TransactionId = "transactionIdentifier"
            };
        }
    
        #endregion

        #region Cooperative
        
        [HttpPost]
        [Route("channel/close/cooperative")]
        public async Task<TransactionResultDto> CloseCooperativeChannel([FromBody] ChannelCloseCooperativeDto closeDto)
        {
            var transactionId = await _contract.CloseChannelCooperative(closeDto);
            return new TransactionResultDto
            {
                TransactionId = transactionId
            };
        }
        
        [HttpPost]
        [Route("channel/close/cooperative/sign")]
        public SignatureResponse SignCloseCooperativeChannel([FromBody] CooperativeSignDto signDto)
        {
            var signature = EthereumUtils.SignCooperativeClose(signDto,  _config.PrivateKey);  
            
            return new SignatureResponse
            {
                SenderSignature = signature
            };
        }
        
        [HttpPost]
        [Route("channel/close/cooperative/verify")]
        public VerifyResultDto VerifyCloseCooperativeChannel([FromBody] CooperativeVerifyDto verifyDtoDto)
        {
            var result = EthereumUtils.VerifyCooperativeClose(verifyDtoDto);  
            
            return new VerifyResultDto
            {
                Result = result
            };
        }
        
        #endregion

        #region Audit

        
        [HttpPost]
        [Route("channel/close/audit")]
        public TransactionResultDto CloseAudit([FromBody] CloseAuditDto audit)
        {
            return new TransactionResultDto
            {
                TransactionId = "transactionIdentifier"
            };
        }
        
                
        [HttpPost]
        [Route("channel/close/audit/sign")]
        public SignatureResponse SignCloseAuditChannel([FromBody] AuditSignDto signDto)
        {
            var signature = EthereumUtils.SignAuditClose(signDto,  _config.PrivateKey);  
            
            return new SignatureResponse
            {
                SenderSignature = signature
            };
        }
        
        [HttpPost]
        [Route("channel/close/audit/verify")]
        public VerifyResultDto VerifyCloseAuditChannel([FromBody] AuditVerifyDto verifyDtoDto)
        {
            var result = EthereumUtils.VerifyAuditClose(verifyDtoDto);  
            
            return new VerifyResultDto
            {
                Result = result
            };
        }

        
        #endregion

        #region Read

        
        [HttpGet]
        [Route("channel/info/{channelId}")]
        public async Task<ChannelInfoDto> GetInfo(string channelId)
        {
            return await _contract.GetChannelInfo(channelId);
        }
        
        [HttpGet]
        [Route("transaction/{transactionId}")]
        public async Task<JsonResult> GetTransaction(string transactionId)
        {
            var result = await _contract.GetTransactionStatus(transactionId);
            return Json( 
                new
                {
                    status = result
                });
        }
       
        #endregion
        
    }
}