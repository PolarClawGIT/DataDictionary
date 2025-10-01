using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for Binding RowState.
    /// </summary>
    class BindingRowStateEnumeration : Enumeration<BindingRowState, BindingRowStateEnumeration>
    {
        /// <summary>
        /// Internal Constructor for Binding RowState Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        BindingRowStateEnumeration(BindingRowState value, String name) : base(value, name) { }

        static BindingRowStateEnumeration()
        {
            List<BindingRowStateEnumeration> data = new List<BindingRowStateEnumeration>()
            {
                new BindingRowStateEnumeration(BindingRowState.Null,       String.Empty)  { DisplayName = "not defined" },
                new BindingRowStateEnumeration(BindingRowState.Detached,   "Detached"),
                new BindingRowStateEnumeration(BindingRowState.Unchanged,  "Unchanged"),
                new BindingRowStateEnumeration(BindingRowState.Added,      "Added"),
                new BindingRowStateEnumeration(BindingRowState.Deleted,    "Deleted"),
                new BindingRowStateEnumeration(BindingRowState.Modified,   "Modified"),
                new BindingRowStateEnumeration(BindingRowState.Historic,   "Historic"),
            };

            BuildDictionary(data);
        }
    }
}
