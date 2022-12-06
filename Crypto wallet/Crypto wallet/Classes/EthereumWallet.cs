using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class EthereumWallet : Wallet
    {
        public List<Guid> AddressesOfNonFungibleAssets { get; set; }
        public List<Guid> AddressesOfSupportedFungAndNonFungAssets { get; }


        public EthereumWallet(Dictionary<Guid, int> Balances, List<Guid> NFAAddresses, List<Guid> SupportedFANFAList) : base(Balances)
        {
            AddressesOfNonFungibleAssets = NFAAddresses;
            AddressesOfSupportedFungAndNonFungAssets= SupportedFANFAList;
        }
        public override double ReturnBalanceOfFA(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            double balance = 0;
            foreach(var nfa in NFAList)
            {
                if (AddressesOfNonFungibleAssets.Contains(nfa.Address))
                {
                    balance = balance + nfa.Value;
                }
            }
            return base.ReturnBalanceOfFA(FAList, NFAList) + balance;
        }
        public override void PrintWallet()
        {
            base.PrintWallet();
            foreach(var guid in AddressesOfNonFungibleAssets)
            {
                Console.WriteLine(guid);
            }
        }
    }
}
