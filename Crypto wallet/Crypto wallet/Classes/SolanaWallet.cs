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
            double balance = 0;
            foreach (var nfa in NFAList)
            {
                if (AddressesOfNonFungibleAssets.Contains(nfa.Address))
                {
                    balance = balance + nfa.Value;
                }
            }
            return base.ReturnBalanceOfFA(FAList, NFAList) + balance;
        }
        public override void PrintAssets(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            foreach (var addressOfNFA in AddressesOfNonFungibleAssets)
            {
                var nfa = FindNFA(NFAList, addressOfNFA.ToString());
                Console.WriteLine($"\nNon fungible asset: \nAddress: {nfa.Address} \nName: {nfa.Name} \nValue of NFA: {Math.Round(nfa.Value, 2)}$");
            }
            base.PrintAssets(FAList, NFAList);
        }
        public NonFungibleAsset FindNFA(List<NonFungibleAsset> NFAList, string address)
        {
            foreach (var nfa in NFAList)
            {
                if (nfa.Address.ToString() == address)
                    return nfa;
            }
            return null;
        }
        public override bool ContainsNFA(string assetId)
        {
            foreach (var addressNFA in AddressesOfNonFungibleAssets)
                if (addressNFA.ToString() == assetId)
                    return true;
            return base.ContainsNFA(assetId);
        }
        public override bool DeletingNFA(Guid addressNFA)
        {
            foreach (var address in AddressesOfNonFungibleAssets)
            {
                if (address == addressNFA)
                {
                    AddressesOfNonFungibleAssets.Remove(address);
                    return true;
                }
            }
            return false;
        }
    }
}
