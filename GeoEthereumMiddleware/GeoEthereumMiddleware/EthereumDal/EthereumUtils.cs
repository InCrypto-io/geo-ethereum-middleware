using System.Numerics;
using System.Text;
using GeoEthereumMiddleware.Models;
using Nethereum.ABI.Encoders;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;
using Nethereum.Signer;
using Nethereum.Util;
using Org.BouncyCastle.Asn1;
using Remotion.Linq.Clauses.ResultOperators;

namespace GeoEthereumMiddleware.EthereumDal
{
    public class EthereumUtils
    {

        #region  Sign

        public static string SignReceipts(ReceiptDto receiptDto, string privateKey)
        {
            var resultHash = Sha3ForReceipts(receiptDto);
            var ms = new MessageSigner();
            var signature = ms.Sign(resultHash.HexToByteArray(), privateKey);      

            return signature;
        }
        
        public static string SignCooperativeClose(CooperativeSignDto cooperativeDto, string privateKey)
        {
            var resultHash = Sha3ForCooperative(cooperativeDto);
            var ms = new MessageSigner();
            var signature = ms.Sign(resultHash.HexToByteArray(), privateKey);      

            return signature;
        }
        
        public static string SignAuditClose(AuditSignDto signDto, string privateKey)
        {
            var resultHash = Sha3ForAudit(signDto);
            var ms = new MessageSigner();
            var signature = ms.Sign(resultHash.HexToByteArray(), privateKey);      

            return signature;
        }
        

        #endregion

        #region  Verify

        public static bool VerifyReceipts(VerifyReceiptDto verifyDto)
        {
            var resultHash = Sha3ForReceipts(verifyDto);
            
            var ms = new MessageSigner();
            var address = ms.EcRecover(resultHash.HexToByteArray(), verifyDto.Signature);      

            return address == verifyDto.Address;
        }
        
        public static bool VerifyCooperativeClose(CooperativeVerifyDto verifyDto)
        {
            var resultHash = Sha3ForCooperative(verifyDto);
            
            var ms = new MessageSigner();
            var address = ms.EcRecover(resultHash.HexToByteArray(), verifyDto.Signature);      

            return address == verifyDto.Address;
        }
        
        public static bool VerifyAuditClose(AuditVerifyDto verifyDto)
        {
            var resultHash = Sha3ForAudit(verifyDto);
            
            var ms = new MessageSigner();
            var address = ms.EcRecover(resultHash.HexToByteArray(), verifyDto.Signature);      

            return address == verifyDto.Address;
        }
        

        #endregion
        
        #region SHA3
        
        public static string Sha3ForReceipts(ReceiptDto receiptDto)
        {
            var sha3 = new Sha3Keccack();
                        
            var rec =
                new IntTypeEncoder()
                    .Encode(receiptDto.ReceiptId);
            var epo =
                new IntTypeEncoder()
                    .Encode(receiptDto.EpochId);
            var amo =
                new IntTypeEncoder()
                    .Encode(StringToBigInteger(receiptDto.AmountWei));
            var output = sha3.CalculateHashFromHex(rec.ToHex(), epo.ToHex(), amo.ToHex(), receiptDto.Address);
            var resultHash = "0x" + output;
           
            
            return resultHash;
        }
        
        public static string Sha3ForCooperative(CooperativeSignDto cooperativeDto)
        {
            var sha3 = new Sha3Keccack();
            
            var bob =
                new IntTypeEncoder()
                    .Encode(StringToBigInteger(cooperativeDto.BobAmountWei));
            var alice =
                new IntTypeEncoder()
                    .Encode(StringToBigInteger(cooperativeDto.AliceAmountWei));

            var output = sha3.CalculateHashFromHex(bob.ToHex(), alice.ToHex(), cooperativeDto.ChannelId);
            var resultHash = "0x" + output;
           
            
            return resultHash;
        }
        
        public static string Sha3ForAudit(AuditSignDto auditDto)
        {
            var sha3 = new Sha3Keccack();
            
            var bob =
                new IntTypeEncoder()
                    .Encode(StringToBigInteger(auditDto.BobAmountWei));
            var alice =
                new IntTypeEncoder()
                    .Encode(StringToBigInteger(auditDto.AliceAmountWei));
            var epoch =
                new IntTypeEncoder()
                    .Encode(auditDto.EpochId);
            var output = sha3.CalculateHashFromHex(bob.ToHex(), alice.ToHex(), auditDto.ChannelId, epoch.ToHex());
            var resultHash = "0x" + output;
           
            
            return resultHash;
        }

        public static string GetChannelAddress(string aliceAddress, string bobAddress)
        {
            var sha3 = new Sha3Keccack();
            
            var output = sha3.CalculateHashFromHex(aliceAddress, bobAddress);
            var resultHash = "0x" + output;
           
            return resultHash;
        }
        #endregion

        #region  Helpers

        public static int StringToInt(string number)
        {
            return int.Parse(number);
        }
        
        public static BigInteger StringToBigInteger(string number)
        {
            return BigInteger.Parse(number);
        }

        public static decimal StringToDecimal(string number)
        {
            return decimal.Parse(number);
        }

        #endregion
        
        public static string GetAddressFromPrivateKey(string privateKey)
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            return account.Address;
        }
         
    }
}