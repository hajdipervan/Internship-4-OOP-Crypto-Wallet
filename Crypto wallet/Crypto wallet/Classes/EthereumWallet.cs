using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class EthereumWallet : Wallet
    {
        public string AddressOfNonFungibleAssets { get; set; }
        public string AddressOfFungAndNonFungAssets { get; } = "Etherum";


        public EthereumWallet() : base() { }

    }
}
