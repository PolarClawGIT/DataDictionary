using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    /// <summary>
    /// Parse methods to handled specific data.
    /// </summary>
    public static class BindingItemParsers
    {
        /// <summary>
        /// Boolean Parser that accepts Strings that contain common boolean terms and returns a Boolean value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Boolean is not IParsable so a specific routine is needed</remarks>
        public static Boolean BooleanTryParse(String value, out Boolean result)
        {
            if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException(nameof(value)); }

            if (value is "yes" or "YES" or "Yes" or "true" or "TRUE" or "True") { result = true; return true; }
            if (value is "no" or "NO" or "No" or "false" or "FALSE" or "False") { result = false; return true; }
            return Boolean.TryParse(value, out result);
        }
    }
}
