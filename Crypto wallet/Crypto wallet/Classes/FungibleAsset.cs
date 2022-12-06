using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_wallet.Classes
{
    public class FungibleAsset
    {
        public Guid Address { get; }
        public string Name { get; }
        public string ShortName { get; set; }
        public double ValueAgainstUSD{get; private set;}

        public FungibleAsset(string name,string shortName, double value)
        {
            Address= Guid.NewGuid();
            Name =name;
            ShortName = shortName;
            ValueAgainstUSD=value;

        }
        public string GuidToString()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString; 
        }
        public double ReturnValue(Guid id)
        {
            if(id!=Address) return 0;
            return ValueAgainstUSD;
        }
        
    }
}
