using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class Transaction
    {
        public Guid Id { get;  }
        public Guid AddressOfAsset { get; }
        public DateTime Date { get; }
        public Guid SentersWalletAdress { get; }
        public Guid ReceiversWalletAdress { get; }
        public bool Revoked { get; set; }
        public Transaction(Guid addressOfAsset, Guid sentersWalletAdress, Guid receiversWalletAdress)
        {
            Id = Guid.NewGuid();
            AddressOfAsset = addressOfAsset;
            Date = DateTime.Now;
            SentersWalletAdress = sentersWalletAdress;
            ReceiversWalletAdress = receiversWalletAdress;
            Revoked= false;
        }
        public virtual void PrintFA(FungibleAsset FA)
        {

        }
    }
}
