using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class Transactions
    {
        public Guid Id { get;  }
        public Guid AddressOfAsset { get; }
        public DateTime Date { get; }
        public Guid SentersWalletAdress { get; }
        public Guid ReceiversWalletAdress { get; }
        public bool Revoked { get; set; }
        public Transactions(Guid addressOfAsset, Guid sentersWalletAdress, Guid receiversWalletAdress)
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
        public bool TransactionNFARevoked(List<Wallet> wallets, Wallet senterWallet, NonFungibleAsset NFA)
        {
            Wallet receiverWallet = null;
            foreach(var w in wallets)
            {
                if (w.Address==ReceiversWalletAdress)
                    receiverWallet = w;
            }
            receiverWallet.DeletingNFA(AddressOfAsset);
            Revoked = true;
            return true;
        }
        public virtual bool TransactionFARevoked(List<Wallet> wallets, Wallet senterWallet, FungibleAsset fa)
        {
            return true;
        }
    }
}
