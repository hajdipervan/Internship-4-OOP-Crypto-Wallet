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
        public Dictionary<Guid, double> BalanceFungibleAssets { get; }
        public List<Guid> ListOfAddressTransactions { get; }
        public Wallet()
        {
            Address= Guid.NewGuid();
            BalanceFungibleAssets= new Dictionary<Guid, double>();
            ListOfAddressTransactions= new List<Guid>(); 

        }
        public double ReturnBalanceOfAssets()
        {
            double balance = 0;
            foreach (var item in BalanceFungibleAssets)
                balance = balance + item.Value;
            return balance;
        }
    }
}
