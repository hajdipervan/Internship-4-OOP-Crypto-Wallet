using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class NonFungibleAsset
    {
        public Guid Address { get; }
        public string Name { get; set; }
        public double Value { get; set; }
        public Guid AddressFungibleAsset { get; }


        public NonFungibleAsset(string name, Guid addressFungibleAsset, double value)
        {
            Address = Guid.NewGuid();
            Name = name;
            AddressFungibleAsset = addressFungibleAsset;
            Value = value;

        }
    }
}
