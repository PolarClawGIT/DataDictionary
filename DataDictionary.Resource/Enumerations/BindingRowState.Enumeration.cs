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
    public class BindingRowStateEnumeration : Enumeration<BindingRowState, BindingRowStateEnumeration>
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

    public static class BindingRowStateExtension
    {
        /// <summary>
        /// Allows conversion from a DataRowState to a BindingRowState
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BindingRowState AsBindingRowState (this DataRowState source)
        { return (BindingRowState)((Int32)source); }

        public static DataRowState AsDataRowState (this BindingRowState source)
        {
            // This should throw an exception.
            return Enum.GetValues<DataRowState>().FirstOrDefault(w => ((Int32)w).Equals((Int32)source));
                
         

            
        }
    }
}
