using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class SolanaWallet: Wallet
    {
        public List<Guid> AddressesOfNonFungibleAssets { get; set; }
        public List<Guid> AddressesOfSupportedFungAndNonFungAssets { get; }
        public SolanaWallet(Dictionary<Guid, int> Balances, List<Guid> NFAAddresses, List<Guid> SupportedFANFAList) : base(Balances)
        {
            AddressesOfNonFungibleAssets = NFAAddresses;
            AddressesOfSupportedFungAndNonFungAssets = SupportedFANFAList;
        }
        public override double ReturnBalanceOfFA(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            return base.ReturnBalanceOfFA(FAList, NFAList);
        }
    }
}
