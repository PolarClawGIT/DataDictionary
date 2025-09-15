using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    static partial class RowStateExtension
    {
        /// <summary>
        /// Gets the Navigation information for the Binding RowState enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IRowStateEnumeration GetNavigation(this BindingRowState value)
        { return Enumeration.GetValue(value); }
    }
}
