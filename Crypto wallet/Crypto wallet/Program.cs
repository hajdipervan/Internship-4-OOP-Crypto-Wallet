using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            var TransactionList = new List<Transaction>();
            

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
                        var choice2 = Answer("Insert wallet address for more options");
                        switch(IsWalletAddress(Wallets, choice2))
                        { 
                           case true:
                                string choice22;
                                do
                                {
                                    choice22 = Answer("1 - Portfolio \n2 - Transfer \n3 - History of transaction \n4 - Back on main menu");
                                    foreach (var wallet in Wallets)
                                    {
                                        wallet.PrintWallet();
                                    }
                                    switch (choice22)
                                    {
                                        case "1":
                                            var searchWallet = FindWallet(choice2, Wallets);
                                            Console.WriteLine($"Total balance: {searchWallet.ReturnBalanceOfFA(FungibleAssetsList, NonFungibleAssetsList)}");
                                            searchWallet.PrintAssets(FungibleAssetsList, NonFungibleAssetsList);
                                            break;
                                        case "2":
                                            var SenterWallet = FindWallet(choice2, Wallets);
                                            if(Transfer(FungibleAssetsList, NonFungibleAssetsList, Wallets, SenterWallet, TransactionList) == true)
                                            {
                                                Console.WriteLine("Transaction completed successfully");
                                                break;
                                            }
                                            Console.WriteLine("Transaction  is not completed");
                                            break;
                                        case "3":
                                            LoadTransactions(TransactionList, FungibleAssetsList, NonFungibleAssetsList);
                                            break;
                                        case "4":
                                            Console.WriteLine("Back on main selected");
                                            choice22 = "5";
                                            break;
                                        default:
                                            Console.WriteLine("Wrong input");
                                            break;
                                    }
                                } while (choice22 != "5");
                                break;
                           default:
                                 Console.WriteLine("Wrong input");
                                 break;
                        }
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
                wallets.Add(new EthereumWallet(ReturnBalanceDictionary(FAList, i), ReturnAddresesNFA(NFAList, i, 0), ReturnSupportedFANFAAdressesList(NFAList, FAList)));
            for (int i = 0; i < 3; i++)
                wallets.Add(new SolanaWallet(ReturnBalanceDictionary(FAList, i), ReturnAddresesNFA(NFAList, i, 9), ReturnSupportedFANFAAdressesList(NFAList, FAList)));
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
            if (i == 2)
                balances.Add(FAList[9].Address, r.Next(1, 11));
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
        static List<Guid> ReturnAddresesNFA(List<NonFungibleAsset> NFAList, int i, int z)
        {
            var NFAAddreses = new List<Guid>();
            for (int j = 0; j < 3; j++)
            {
                NFAAddreses.Add(NFAList[i *3+ j+z].Address);
            }
            if (i == 2)
                if (z == 0)
                    NFAAddreses.Add(NFAList[18].Address);
                else
                    NFAAddreses.Add(NFAList[19].Address);
            
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
                            question = Answer("Solana wallet creation selected \nAre you sure you want to create new Solana crypto wallet? y/n");
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
                var name = ReturnTypeOfWallet(wallet);
                var balance = Math.Round(wallet.ReturnBalanceOfFA(FAList, NFAList), 2);
                if (wallet is BitcoinWallet)
                    name = "Bitcoin";
                else if (wallet is EthereumWallet)
                    name = "Ethereum";
                else
                    name = "Solana";
                Console.WriteLine($"\n{name} wallet \nAddress: {wallet.Address} \nTotal balance: {balance,2}$ ");
                wallet.ListHistoryBalances.Add(balance);
                wallet.HistoryBalancesPercentage();
            }
        }
        static bool IsWalletAddress(List<Wallet> wallets, string address)
        {
            foreach (var wallet in wallets)
            {
                if (wallet.Address.ToString() == address) return true;
            }
            return false;
        }
        static Wallet FindWallet(string address, List<Wallet> wallets)
        {
            foreach (var wallet in wallets)
                if (wallet.Address.ToString() == address) return wallet;
            return null;
        }
        static string ReturnTypeOfWallet(Wallet wallet)
        {
            if (wallet is BitcoinWallet)
                return "Bitcoin";
            if (wallet is EthereumWallet)
                return "Ethereum";
            return "Solana";
        }
        static bool Transfer(List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList, List<Wallet> wallets, Wallet SenterWallet, List<Transaction> Transactions)
        {
            var walletAddressReceiver = Answer("Insert address of wallet that receives");
            if (IsWalletAddress(wallets, walletAddressReceiver) == false)
            {
                Console.WriteLine("Typed wallet address doesn't exist");
                return false;
            }
            var ReceiverWallet = FindWallet(walletAddressReceiver, wallets);
            if (SenterWallet == ReceiverWallet)
            {
                Console.WriteLine("You inserted the same senter wallet and receiver wallet");
                return false;

            }
            var typeOfWallet = ReturnTypeOfWallet(SenterWallet);
            var assetId = Answer("Insert ID of asset");
            if (SenterWallet.FindFA(FAList, assetId) == null && SenterWallet.ContainsNFA(assetId)==false)
            {
                Console.WriteLine("Wallet senter does not contain inserted asset");
                return false;
            }
            if (SenterWallet.FindFA(FAList, assetId) != null)
            {

                int amount = CheckIsInteger();
                if (amount == 0)
                {
                    Console.WriteLine("You didn't insert integer or you inserted 0");
                    return false;
                }
                Guid AssetIdGuid;
                Guid.TryParse(assetId, out AssetIdGuid);
                if (SenterWallet.BalanceFungibleAssets[AssetIdGuid] < amount)
                {
                    Console.WriteLine("It is not possible to finish the transfer (There is not enough funds) ");
                    return false;
                }
                string agree = Answer("Are you sure you want to make the transaction? y/n");
                if (agree != "y")
                {
                    Console.WriteLine("You have canceled the transaction");
                    return false;
                }
                var balanceSenterStart = SenterWallet.ReturnBalanceOfFA(FAList, NFAList);
                int newValueSenter = SenterWallet.BalanceFungibleAssets[AssetIdGuid] - amount;
                SenterWallet.BalanceFungibleAssets[AssetIdGuid] = newValueSenter;
                var balanceReceiverStart = ReceiverWallet.ReturnBalanceOfFA(FAList, NFAList);
                if (ReceiverWallet.ContainsId(AssetIdGuid))
                {
                    int newValueReceiver = ReceiverWallet.BalanceFungibleAssets[AssetIdGuid] + amount;
                    ReceiverWallet.BalanceFungibleAssets[AssetIdGuid] = newValueReceiver;
                    var balanceSenterEnd = SenterWallet.ReturnBalanceOfFA(FAList, NFAList);
                    var balanceReceiverEnd = SenterWallet.ReturnBalanceOfFA(FAList, NFAList);
                    FungibleAssetTransaction FATransaction1 = new FungibleAssetTransaction(AssetIdGuid, SenterWallet.Address, ReceiverWallet.Address, balanceSenterStart, balanceReceiverStart, balanceSenterEnd, balanceReceiverEnd);
                    SenterWallet.ListOfAddressTransactions.Add(FATransaction1.Id);
                    ReceiverWallet.ListOfAddressTransactions.Add(FATransaction1.Id);
                    Transactions.Add(FATransaction1);
                    return true;
                }
                ReceiverWallet.BalanceFungibleAssets.Add(AssetIdGuid, amount);
                var balanceSenterEnd1 = SenterWallet.ReturnBalanceOfFA(FAList, NFAList);
                var balanceReceiverEnd1 = SenterWallet.ReturnBalanceOfFA(FAList, NFAList);
                FungibleAssetTransaction FATransaction = new FungibleAssetTransaction(AssetIdGuid, SenterWallet.Address, ReceiverWallet.Address, balanceSenterStart, balanceReceiverStart, balanceSenterEnd1, balanceReceiverEnd1);
                SenterWallet.ListOfAddressTransactions.Add(FATransaction.Id);
                ReceiverWallet.ListOfAddressTransactions.Add(FATransaction.Id);
                Transactions.Add(FATransaction);
                return true;
            }
            
            // smatramo da je nfa
            if (ReceiverWallet is EthereumWallet || ReceiverWallet is SolanaWallet)
            {
                Guid NFAIdGuid;
                Guid.TryParse(assetId, out NFAIdGuid);
                NonFungibleAsset NFAForTransaction = ReturnNFA(NFAList, NFAIdGuid);
                SenterWallet.DeletingNFA(NFAIdGuid);
                ReceiverWallet.AddingNFAInList(NFAForTransaction);
                NonFungibleAssetTransaction NFATransaction = new NonFungibleAssetTransaction(NFAIdGuid, SenterWallet.Address, ReceiverWallet.Address);
                Transactions.Add(NFATransaction);
                SenterWallet.ListOfAddressTransactions.Add(NFATransaction.Id);
                ReceiverWallet.ListOfAddressTransactions.Add(NFATransaction.Id);
                FungibleAsset FAForChanging = ReturnFA(FAList, NFAForTransaction.AddressFungibleAsset);
                FAForChanging.ChangeValue();
                return true;
            }
            Console.WriteLine("Transaction is not possible (Type of wallet cannot receive non fungible assets)");
            return false;

        }
        static bool ContainsAddressForFA(List<FungibleAsset> FAList, string assetId)
        {
            foreach(var fa in FAList)
            {
                if (fa.Address.ToString() == assetId)
                    return true;
            }
            return false;
        }
        static NonFungibleAsset ReturnNFA(List<NonFungibleAsset> NFAList, Guid address)
        {
            foreach(var nfa in NFAList)
            {
                if(nfa.Address==address) return nfa;
            }
            return null;
        }
        static FungibleAsset ReturnFA(List<FungibleAsset> FAList, Guid address)
        {
            foreach (var fa in FAList)
            {
                if (fa.Address == address) return fa;
            }
            return null;
        }
        static bool LoadTransactions(List<Transaction> Transactions, List<FungibleAsset> FAList, List<NonFungibleAsset> NFAList)
        {
            List<Transaction> ReverseList = ReturnReverseListTransaction(Transactions);
            foreach (var t in ReverseList)
            {
                Console.WriteLine($"\nTransaction id: {t.Id} \nDate and time of transaction: {t.Date} \nAddress of senter wallet: {t.SentersWalletAdress} \n" +
                    $"Address of receiver wallet: {t.ReceiversWalletAdress}");
                if(t is FungibleAssetTransaction)
                {
                    FungibleAsset FA = ReturnFA(FAList, t.AddressOfAsset);
                    t.PrintFA(FA);
                    Console.WriteLine($"Is revoked: {t.Revoked}");
                    return true;
                }
                NonFungibleAsset NFA = ReturnNFA(NFAList, t.AddressOfAsset);
                Console.WriteLine($"Name of nonfungible asset: {NFA.Name}");
                Console.WriteLine($"Is revoked: {t.Revoked}");
            }
            return true ;
        }
        static int CheckIsInteger()
        {
            try
            {
                int amount = int.Parse(Answer("Insert amount of FA for sending"));
                return amount;
            }
            catch
            {
                return 0; 
            }
        }
        static List<Transaction> ReturnReverseListTransaction(List<Transaction> transactions)
        {
            var transactionsReverseList = new List<Transaction>();
            for(int i = transactions.Count()-1; i > -1; i--)
            {
                transactionsReverseList.Add(transactions[i]);
            }
            return transactionsReverseList;
        }
    }
}