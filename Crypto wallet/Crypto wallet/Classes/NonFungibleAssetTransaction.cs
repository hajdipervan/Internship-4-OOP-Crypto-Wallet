using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class NonFungibleAssetTransaction : Transaction
    {
        public NonFungibleAssetTransaction(Guid addressOfAsset, Guid sentersWalletAdress, Guid receiversWalletAdress):base( addressOfAsset,  sentersWalletAdress,  receiversWalletAdress) { }
    }
}
