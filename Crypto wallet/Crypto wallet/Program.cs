using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Crypto_wallet.Classes;
namespace Crypto_wallet
{
    class Program
    {
        static void Main(string[] args)
        {
            var FungibleAssetsList = LoadFungibleAssets();
            var NonFungibleAssetsList=LoadNonFungibleAssets();
            var Wallets = new List<Wallet>();
            string choice;
            do
            {
                choice = Answer("\n1 - Kreiraj wallet \n2 - Pristupi walletu");
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Odabrano kreiranje walleta");
                        Wallets=CreateWallet(Wallets);
                        break;
                    case "2":
                        Console.WriteLine("Odabran pristup walletu");
                        ShowWallets(Wallets);
                        break;
                    default:
                        Console.WriteLine("Krivi unos");
                        break;
                }
            } while (choice != "3");
        }
        static string Answer(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
        static List<FungibleAsset> LoadFungibleAssets()
        {
            var FungibleAssets = new List<FungibleAsset>
            {
                new FungibleAsset("bitcoin","BTC", 17106.60),
                new FungibleAsset("ethereum","ETH", 1275.73),
                new FungibleAsset("cardano","ADA", 0.32),
                new FungibleAsset("polygon","MATIC", 0.9185),
                new FungibleAsset("near","NR", 1.69),
                new FungibleAsset("dogecoin","DOGE", 0.10),
                new FungibleAsset("solana","SOL", 14.16),
                new FungibleAsset("polkadot","DOT", 5.57 ),
                new FungibleAsset("litecoin","LTC", 76.76),
                new FungibleAsset("chainlink","LINK", 7.68)
            };
            return FungibleAssets;
        }
        static List<NonFungibleAsset> LoadNonFungibleAssets()
        {
            var NonFungibleAssets = new List<NonFungibleAsset> { };
            return NonFungibleAssets;
        }
        static List<Wallet> CreateWallet(List<Wallet> wallets)
        {
            string choice1;
            do
            {
                choice1 = Answer("\n1 - Bitcoin wallet \n2 - Ethereum wallet \n3 - Solana wallet");
                switch (choice1)
                {
                    case "1":
                        Console.WriteLine("Odabrano kreiranje Bitcoin walleta");
                        var newBitcoinWallet = new BitcoinWallet();
                        wallets.Add(newBitcoinWallet);
                        Console.WriteLine("Uspjesno kreirano");
                        return wallets;
                    case "2":
                        Console.WriteLine("Odabrano kreiranje Ethereum walleta");
                        var newEthereumWallet = new EthereumWallet();
                        wallets.Add(newEthereumWallet);
                        Console.WriteLine("Uspjesno kreirano");
                        return wallets;
                    case "3":
                        Console.WriteLine("Odabrano kreiranje Solana walleta");
                        var newSolanaWallet = new SolanaWallet();
                        wallets.Add(newSolanaWallet);
                        Console.WriteLine("Uspjesno kreirano");
                        return wallets;
                    default:
                        Console.WriteLine("Krivi unos");
                        break;
                }
            } while (choice1 != "4");
            return wallets;
        }
        static void ShowWallets(List<Wallet> wallets)
        {
            foreach(var wallet in wallets)
            {
                if(wallet is BitcoinWallet)
                    Console.WriteLine($"Bitcoin wallet \nAddress: {wallet.Address} \nTotal balance: {wallet.ReturnBalanceOfAssets()} ");
                else if(wallet is EthereumWallet)
                    Console.WriteLine($"Ethereum wallet \nAddress: {wallet.Address} \nTotal balance: {wallet.ReturnBalanceOfAssets()} ");
                else
                    Console.WriteLine($"Solana wallet \nAddress: {wallet.Address} \nTotal balance: {wallet.ReturnBalanceOfAssets()} ");
            }
        }
    }
}