using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto_wallet.Classes;

namespace Crypto_wallet.Classes
{
    public class FungibleAssetTransaction : Transactions
    {
        public double SenterStartBalanceWallet;
        public double SenterEndBalanceWallet;
        public double ReceiverStartBalanceWallet;
        public double ReceiverEndBalanceWallet;

        public FungibleAssetTransaction(Guid addressOfAsset, Guid sentersWalletAdress, Guid receiversWalletAdress, double senterStartBalanceWallet,  double receiverStartBalanceWallet,
            double senterEndBalanceWallet, double receiverEndBalanceWallet) : base(addressOfAsset, sentersWalletAdress, receiversWalletAdress)
        {
            SenterStartBalanceWallet = senterStartBalanceWallet;
            ReceiverStartBalanceWallet = receiverStartBalanceWallet;
            SenterEndBalanceWallet = senterEndBalanceWallet;
            ReceiverEndBalanceWallet = receiverEndBalanceWallet;
        }
        public override void PrintFA(FungibleAsset FA)
        {
            //base.PrintFA(FA);
            double amountOfFA = (SenterStartBalanceWallet - SenterEndBalanceWallet) / FA.ValueAgainstUSD;
            Console.WriteLine($"Amount: {Math.Round(amountOfFA, 1)} \nName of fungible asset: {FA.Name}");
        }
        public override bool TransactionFARevoked(List<Wallet> wallets, Wallet senterWallet, FungibleAsset fa )
        {
            Wallet receiverWallet = null;
            double amountOfFA = Math.Round((SenterStartBalanceWallet - SenterEndBalanceWallet) / fa.ValueAgainstUSD, 1);
            foreach (var w in wallets)
            {
                if (w.Address == ReceiversWalletAdress)
                {
                    receiverWallet = w;
                }
            }
            if (senterWallet.BalanceFungibleAssets.ContainsKey(AddressOfAsset))
            {
                // povecaj amount
                senterWallet.BalanceFungibleAssets[AddressOfAsset] = senterWallet.BalanceFungibleAssets[AddressOfAsset]+(int)amountOfFA;
                receiverWallet.BalanceFungibleAssets[AddressOfAsset] = receiverWallet.BalanceFungibleAssets[AddressOfAsset] - (int)amountOfFA;
                return true;
            }
            senterWallet.BalanceFungibleAssets.Add(AddressOfAsset,(int)amountOfFA );
            receiverWallet.BalanceFungibleAssets[AddressOfAsset]= receiverWallet.BalanceFungibleAssets[AddressOfAsset] - (int)amountOfFA;
            return true;
        }

    }
}
