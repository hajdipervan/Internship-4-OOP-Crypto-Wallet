using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            var NonFungibleAssetsList=LoadNonFungibleAssets(FungibleAssetsList);
            var Wallets = LoadWallets(FungibleAssetsList, NonFungibleAssetsList);
            string choice;
            do
            {
                choice = Answer("\n1 - Create wallet \n2 - Wallet access");
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Wallet creation selected");
                        Wallets=CreateWallet(Wallets, FungibleAssetsList, NonFungibleAssetsList);
                        break;
                    case "2":
                        Console.WriteLine("Wallet access is selected");
                        ShowWallets(Wallets, FungibleAssetsList, NonFungibleAssetsList);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
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
        static List<NonFungibleAsset> LoadNonFungibleAssets(List<FungibleAsset> FAList)
        {
            var NonFungibleAssets = new List<NonFungibleAsset>
            {
                    new NonFungibleAsset("house", FAList[0].Address, 15*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("flat", FAList[0].Address, 10*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("villa", FAList[1].Address, 500*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("bungalow", FAList[1].Address, 20*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("office", FAList[1].Address, 5*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("flat", FAList[1].Address, 10*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("office", FAList[1].Address, 10*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("farmland", FAList[1].Address, 2*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("construction land", FAList[0].Address, 7*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("duplex", FAList[0].Address, 2*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("apartment", FAList[0].Address, 1000*FAList[8].ValueAgainstUSD),
                    new NonFungibleAsset("hotel", FAList[8].Address, 10000*FAList[8].ValueAgainstUSD),
                    new NonFungibleAsset("house", FAList[0].Address, 10*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("studio apartment", FAList[0].Address, 5*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("Mona Lisa", FAList[0].Address, 51176*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("Irises", FAList[0].Address, 7500*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("Suprematist Composition", FAList[1].Address, 72000*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("Portrait of Doctor Gachet", FAList[0].Address, 5000*FAList[0].ValueAgainstUSD),
                    new NonFungibleAsset("Compositions", FAList[1].Address, 31000*FAList[1].ValueAgainstUSD),
                    new NonFungibleAsset("house", FAList[8].Address, 400*FAList[8].ValueAgainstUSD)
            };
            return NonFungibleAssets;
        }
        static List<Wallet> LoadWallets(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            var wallets = new List<Wallet>();
            //kreirajmo 3 bitcoin walleta
            for (int i = 0; i < 3; i++)
                wallets.Add(new BitcoinWallet(ReturnBalanceDictionary(FAList, i), ReturnSupportedFAAdressesList(FAList)));
            for (int i = 0; i < 3; i++)
                wallets.Add(new EthereumWallet(ReturnBalanceDictionary(FAList, i), ReturnAddresesNFA(NFAList, i), ReturnSupportedFANFAAdressesList(NFAList, FAList)));
            for (int i = 0; i < 3; i++)
                wallets.Add(new SolanaWallet(ReturnBalanceDictionary(FAList, i), ReturnAddresesNFA(NFAList, 9+i), ReturnSupportedFANFAAdressesList(NFAList, FAList)));
            return wallets;

        }
        static Dictionary<Guid, int> ReturnBalanceDictionary(List<FungibleAsset> FAList, int i)
        {
            Random r = new Random();
            var balances = new Dictionary<Guid, int>()
            {
                { FAList[i*3].Address, r.Next(1,11) },
                { FAList[i*3+1].Address, r.Next(1,11) },
                { FAList[i*3+2].Address, r.Next(1,11)}
            };
            return balances;
        }
        static List<Guid> ReturnSupportedFAAdressesList(List<FungibleAsset> FAList)
        {
            var FAGuides=new List<Guid>();
            foreach(var fa in FAList)
            {
                FAGuides.Add(fa.Address);
            }
            return FAGuides;
        }
        static List<Guid> ReturnAddresesNFA(List<NonFungibleAsset> NFAList, int i)
        {
            var NFAAddreses = new List<Guid>();
            for(int j = 0; j < 3; j++)
            {
                NFAAddreses.Add(NFAList[i + j].Address);
            }
            return NFAAddreses;
        }
        static List<Guid> ReturnSupportedFANFAAdressesList(List<NonFungibleAsset> NFAList, List<FungibleAsset> FAList)
        {
            var FAAndNFAGuides = new List<Guid>();
            foreach (var nfa in NFAList)
                FAAndNFAGuides.Add(nfa.Address);
            foreach (var fa in FAList)
                FAAndNFAGuides.Add(fa.Address);
            return FAAndNFAGuides;
        }
        static List<Wallet> CreateWallet(List<Wallet> wallets, List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            string choice1;
            do
            {
                choice1 = Answer("\n1 - Bitcoin wallet \n2 - Ethereum wallet \n3 - Solana wallet");
                var BalanceFungibleAssets = new Dictionary<Guid, int> { };
                var AddressesNFA=new List<Guid>();
                string question;
                switch (choice1)
                {
                    case "1":
                        {
                            question = Answer("Bitcoin wallet creation selected \nAre you sure you want to create new Bitcoin crypto wallet? y/n");
                            switch (question)
                            {
                                case "y":
                                    var newBitcoinWallet = new BitcoinWallet(BalanceFungibleAssets, ReturnSupportedFAAdressesList(FAList));
                                    wallets.Add(newBitcoinWallet);
                                    Console.WriteLine("Successfully created");
                                    return wallets;
                                case "n":
                                    Console.WriteLine("You have given up on entering a new wallet");
                                    return wallets;
                                default:
                                    Console.WriteLine("Wrong input");
                                    break;
                            }
                            break;
                        }
                    case "2":
                        {
                            question = Answer("Ethereum wallet creation selected \nAre you sure you want to create new Ethereum crypto wallet? y/n");
                            switch (question)
                            {
                                case "y":
                                    var newEthereumWallet = new EthereumWallet(BalanceFungibleAssets, AddressesNFA, ReturnSupportedFANFAAdressesList(NFAList, FAList));
                                    wallets.Add(newEthereumWallet);
                                    Console.WriteLine("Successfully created");
                                    return wallets;
                                case "n":
                                    Console.WriteLine("You have given up on entering a new wallet");
                                    return wallets;
                                default:
                                    Console.WriteLine("Wrong input");
                                    break;
                            }
                            break;
                        }
                    case "3":
                        {
                            question = Answer("Solana wallet creation selected \nAre you sure you want to create new Ethereum crypto wallet? y/n");
                            switch (question)
                            {
                                case "y":
                                    Console.WriteLine("Solana wallet creation selected");
                                    var newSolanaWallet = new SolanaWallet(BalanceFungibleAssets, AddressesNFA, ReturnSupportedFANFAAdressesList(NFAList, FAList));
                                    wallets.Add(newSolanaWallet);
                                    Console.WriteLine("Successfully created");
                                    return wallets;
                                case "n":
                                    Console.WriteLine("You have given up on entering a new wallet");
                                    return wallets;
                                default:
                                    Console.WriteLine("Wrong input");
                                    break;
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            } while (choice1 != "4");
            return wallets;
        }
        static void ShowWallets(List<Wallet> wallets, List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            foreach(var wallet in wallets)
            {
                if (wallet is BitcoinWallet)
                {
                    var balance = wallet.ReturnBalanceOfFA(FAList, NFAList);
                    Console.WriteLine($"Bitcoin wallet \nAddress: {wallet.Address} \nTotal balance: {balance}$ ");
                    wallet.HistoryBalancesPercentage(balance);
                    

                }
                else if (wallet is EthereumWallet)
                {
                    var balance = wallet.ReturnBalanceOfFA(FAList, NFAList);
                    Console.WriteLine($"Ethereum wallet \nAddress: {wallet.Address} \nTotal balance: {balance}$ ");
                    wallet.HistoryBalancesPercentage(balance);
                }
                else
                {
                    var balance = wallet.ReturnBalanceOfFA(FAList, NFAList);
                    Console.WriteLine($"Solana wallet \nAddress: {wallet.Address} \nTotal balance: {Math.Round(balance, 2)}$ ");
                    wallet.HistoryBalancesPercentage(balance);

                }

            }
        }
    }
}