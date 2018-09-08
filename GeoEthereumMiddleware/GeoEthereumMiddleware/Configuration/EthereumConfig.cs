namespace GeoEthereumMiddleware.Configuration
{
    public class EthereumConfig
    {
        public string PrivateKey { get; set; }

        public string ContractAddress { get; set; }

        public string ContractABI { get; set; }

        public string GasPriceWei { get; set; }

        public string MaxGasCount { get; set; }

        public string RpcAddress { get; set; }
    }

}