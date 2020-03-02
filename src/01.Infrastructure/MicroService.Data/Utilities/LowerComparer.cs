using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Utilities
{
    public class LowerComparer : IEqualityComparer<string>
    {
        public bool Equals(string a, string b)
        {
            return a.ToLower().Equals(b.ToLower());
        }

        public int GetHashCode(string a)
        {
            return a.GetHashCode();
        }
    }
}
