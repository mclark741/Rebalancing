using System;
using System.Collections.Generic;

namespace Rebalancing.Core
{
    public class Security
    {
        public int SecurityId { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public override string ToString()
        {
            return $"{Symbol} - {Description}";
        }
    }

    public class SecuritySymbolEqualityComparer : IEqualityComparer<Security>
    {
        public bool Equals(Security s1, Security s2)
        {
            if (s2 == null && s1 == null)
                return true;
            else if (s1 == null || s2 == null)
                return false;
            else if (s1.Symbol == s2.Symbol)
                return true;
            else
                return false;
        }

        public int GetHashCode(Security s)
        {
            return s.Symbol.GetHashCode();
        }
    }
}
