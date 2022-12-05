using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class BitcoinWallet : Wallet
    {
        public List<string> AddressOfSupportedFungibleAssets { get; } = new List<string> { "Bitcoin", "Nesto", "Nesto" };

        public BitcoinWallet() : base() {}
        
    }
}
