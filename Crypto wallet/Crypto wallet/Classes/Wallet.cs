using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Crypto_wallet.Classes;

namespace Crypto_wallet.Classes
{
    public class Wallet
    {
        public Guid Address { get; }
        public Dictionary<Guid, int> BalanceFungibleAssets { get; set; }
        public List<Guid> ListOfAddressTransactions { get; set; }
        public List<double> ListHistoryBalances { get; set; }
        public Wallet(Dictionary<Guid, int> Balances)
        {
            Address= Guid.NewGuid();
            BalanceFungibleAssets = Balances;
            ListOfAddressTransactions= new List<Guid>();
            ListHistoryBalances=new List<double> { 0 };
        }
        public virtual double ReturnBalanceOfFA(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            double balance = 0;
            foreach (var item in BalanceFungibleAssets)
                foreach (var fa in FAList)
                {
                    if (fa.Address == item.Key)
                    {
                        balance = balance + item.Value * fa.ValueAgainstUSD;
                    }
                }
            return balance;
        }
        public void HistoryBalancesPercentage()
        {
            if (ListHistoryBalances.Count == 2)
            {
                Console.WriteLine("Percegentage is 0%");
            }
            else
            {
                var number = ListHistoryBalances.Count();
                double percentage =ListHistoryBalances.Last() - ListHistoryBalances[number - 2];
                Console.WriteLine($"Percentage is {Math.Round( percentage/ListHistoryBalances[number - 2] * 100, 2)}%");
            }
        }
        public bool IsWalletAddress(List<Wallet> wallets, string address)
        {
            foreach(var wallet in wallets)
            {
                if (wallet.Address.ToString() == address) return true;
            }
            return false;
        }
        public virtual void PrintWallet()
        {
            Console.WriteLine(Address);
            foreach(var bal in BalanceFungibleAssets)
            {
                Console.WriteLine(bal.Value);
            }
            
        }
        public virtual void PrintAssets(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            foreach (var bfa in BalanceFungibleAssets)
            {
                var fa = FindFA(FAList, bfa.Key.ToString());
                Console.WriteLine($"\nFungible asset:\nAddress: {fa.Address} \nName: {fa.Name} \nShort name: {fa.ShortName} \nValue of FA: {Math.Round(fa.ValueAgainstUSD, 2)}$ \n" +
                    $"Total value in USD: {BalanceFungibleAssets[bfa.Key]* fa.ValueAgainstUSD}$ \nAmount: {bfa.Value}");
            }
            
        }
        public FungibleAsset FindFA(List<FungibleAsset> FAList, string address)
        {
            foreach (var fa in FAList)
            {
                if (fa.Address.ToString() == address)
                    return fa;
            }
            return null;
        }
        public virtual bool ContainsNFA(string assetId)
        {
            return false;
        }
        public Guid AddTransaction()
        {
            Guid TransactionGuid = new Guid();
            ListOfAddressTransactions.Add(TransactionGuid);
            return TransactionGuid;
        }
        public virtual bool DeletingNFA(Guid address)
        {
            return false;
        }
        public virtual void AddingNFAInList(NonFungibleAsset NFA){}
        public bool ContainsId(Guid address)
        {
            foreach (var a in BalanceFungibleAssets)
            {
                if (a.Key == address)
                    return true;
            }
            return false;
        }

    }
}
