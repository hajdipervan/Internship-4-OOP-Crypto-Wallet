using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto_wallet.Classes;

namespace Crypto_wallet.Classes
{
    public class FungibleAssetTransaction : Transaction
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
        public override void PrintFA()
        {
            base.PrintFA();
            Console.WriteLine($"Start balance of senter wallet: {SenterStartBalanceWallet}\n End balance of senter wallet: {SenterEndBalanceWallet}" +
                $"\nStart balance of receiver wallet: {ReceiverStartBalanceWallet}\n End balance of senter wallet: {ReceiverEndBalanceWallet}");
        }

    }
}
