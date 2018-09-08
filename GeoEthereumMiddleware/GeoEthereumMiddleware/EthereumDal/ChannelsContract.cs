using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using GeoEthereumMiddleware.Configuration;
using GeoEthereumMiddleware.Models;
using GeoEthereumMiddleware.Models.ChannelInfo;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using Microsoft.Extensions.Options;
using Nethereum.ABI.Encoders;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Remotion.Linq.Clauses.ResultOperators;

namespace GeoEthereumMiddleware.EthereumDal
{
    public class ChannelsContract
    {
        private readonly EthereumConfig _config;
        private readonly Web3 _web3;
        private readonly Account _account;

        public ChannelsContract(EthereumConfig config)
        {
            _config = config;
            _account = new Account(_config.PrivateKey);
            _web3 = new Web3(_account, _config.RpcAddress);
        }


        public async Task<string> CreateChannel(CreateChannelDto channel)
        {
            var contract = _web3.Eth.GetContract(_config.ContractABI, _config.ContractAddress);
            var openChannelFunction = contract.GetFunction("openChannel");

            var transactionInput = GetTransactionInput(channel.AmountWei);

            var res = await openChannelFunction
                .SendTransactionAsync(transactionInput,
                    channel.ReceiverAddress);

            return res;
        }

        public async Task<string> CreateChannelReply(CreateChannelDto channel)
        {
            var contract = _web3.Eth.GetContract(_config.ContractABI, _config.ContractAddress);
            var openChannelFunction = contract.GetFunction("respondChannel");

            var transactionInput = GetTransactionInput(channel.AmountWei);

            var res = await openChannelFunction
                .SendTransactionAsync(transactionInput,
                    channel.ReceiverAddress);

            return res;
        }

        public async Task<string> CloseChannelCooperative(ChannelCloseCooperativeDto closeDto)
        {
            var contract = _web3.Eth.GetContract(_config.ContractABI, _config.ContractAddress);
            var openChannelFunction = contract.GetFunction("cooperativeClose");

            var transactionInput = GetTransactionInput();

            var balanceAlice = BigInteger.Parse(closeDto.AliceAmountWei);
            var balanceBob = BigInteger.Parse(closeDto.BobAmountWei);
            var res = await openChannelFunction
                .SendTransactionAsync(transactionInput,
                    closeDto.ChannelId.HexToByteArray(),
                    balanceAlice,
                    balanceBob,
                    closeDto.AliceSignature.HexToByteArray(),
                    closeDto.BobSignature.HexToByteArray());

            return res;
        }

        public async Task<string> GetTransactionStatus(string transactionId)
        {
            //TODO: Get Transaction state
            var service = _web3.Eth.Transactions.GetTransactionReceipt;
            var transaction = await service.SendRequestAsync(transactionId);
           
            if (transaction == null) return "PENDING";
             
            return transaction.Status.Value == 0 ? "FAILED" : "CONFIRMED";
        }

        public async Task<ChannelInfoDto> GetChannelInfo(string channelId)
        {
            var contract = _web3.Eth.GetContract(_config.ContractABI, _config.ContractAddress);
            var detailsFunction = contract.GetFunction("getChannelDetails");
            var requestsDetailsFunction = contract.GetFunction("getClosingRequestDetails");
            
            var channelDetails =  await detailsFunction.CallDeserializingToObjectAsync<ChannelDetails>(channelId.HexToByteArray());
            var channelReqquests = await  requestsDetailsFunction.CallDeserializingToObjectAsync<ClosingRequestDetails>(channelId.HexToByteArray());

            return new ChannelInfoDto
            {
                ChannelDetails = ChannelDetailsJson.FromEhterModel(channelDetails),
                ClosingRequestDetails = channelReqquests
            };
        }


        private TransactionInput GetTransactionInput(string valueWei = "0")
        {
            return new TransactionInput()
            {
                Gas = new HexBigInteger(_config.MaxGasCount),
                GasPrice = new HexBigInteger(_config.GasPriceWei),
                From = _account.Address,
                Value = new HexBigInteger(EthereumUtils.StringToBigInteger(valueWei))
            };
        }
    }
}