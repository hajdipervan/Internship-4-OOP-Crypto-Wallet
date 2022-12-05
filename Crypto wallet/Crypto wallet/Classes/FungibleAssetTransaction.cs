using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto_wallet.Classes;

namespace Crypto_wallet.Classes
{
    public class FungibleAssetTransaction : Transaction
    {
        public Dictionary<Guid, double> SenterStartBalanceWallet;
        public Dictionary<Guid, double> SenterEndBalanceWallet;
        public Dictionary<Guid, double> ReceiverStartBalanceWallet;
        public Dictionary<Guid, double> ReceiverEndBalanceWallet;

        public FungibleAssetTransaction(Guid addressOfAsset, Guid sentersWalletAdress, Guid receiversWalletAdress, Dictionary<Guid, double> senterStartBalanceWaller, Dictionary<Guid, double> receiverStartBalanceWallet) : base(addressOfAsset, sentersWalletAdress, receiversWalletAdress)
        {
            SenterStartBalanceWallet = senterStartBalanceWaller;
            ReceiverStartBalanceWallet = receiverStartBalanceWallet;

        }
        
    }
}
