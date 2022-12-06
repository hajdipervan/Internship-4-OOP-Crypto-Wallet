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
        public Dictionary<Guid, int> BalanceFungibleAssets { get; }
        public List<Guid> ListOfAddressTransactions { get; }
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
        public void HistoryBalancesPercentage(double value)
        {
            ListHistoryBalances.Add(value);
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
        public virtual void PrintWallet()
        {
            Console.WriteLine(Address);
            foreach(var bal in BalanceFungibleAssets)
            {
                Console.WriteLine(bal.Value);
            }
            Console.WriteLine(ListOfAddressTransactions);
            
        }
    }
}
