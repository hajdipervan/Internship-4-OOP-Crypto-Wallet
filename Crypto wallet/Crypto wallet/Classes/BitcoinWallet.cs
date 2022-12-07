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
        public List<Guid> AddressOfSupportedFungibleAssets { get; } = new List<Guid> {};

        public BitcoinWallet(Dictionary<Guid, int> Balances, List<Guid> FAAddresses) : base(Balances)
        {
            AddressOfSupportedFungibleAssets = FAAddresses;
        }
      
        
    }
}
