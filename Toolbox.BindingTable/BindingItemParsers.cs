using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public static class BindingItemParsers
    {
        public static Boolean BooleanTryParse(String value, out Boolean result)
        {
            if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException(nameof(value)); }

            if (value is "yes" or "YES" or "Yes" or "true" or "TRUE" or "True") { result = true; return true; }
            if (value is "no" or "NO" or "No" or "false" or "FALSE" or "False") { result = false; return true; }
            return Boolean.TryParse(value, out result);
        }
    }
}
