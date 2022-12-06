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
        public override void PrintAssets(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            foreach (var addressOfNFA in AddressesOfNonFungibleAssets)
            {
                var nfa = FindNFA(NFAList, addressOfNFA);
                Console.WriteLine($"\nNon fungible asset: \nAddress: {nfa.Address} \nName: {nfa.Name} \nValue of NFA: {Math.Round(nfa.Value, 2)}$");
            }
            base.PrintAssets(FAList, NFAList);
        }
        public NonFungibleAsset FindNFA(List<NonFungibleAsset> NFAList, Guid address)
        {
            foreach (var nfa in NFAList)
            {
                if (nfa.Address == address)
                    return nfa;
            }
            return null;
        }
    }
}
