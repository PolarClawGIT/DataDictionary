using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Enumerations
{
    static partial class RowStateExtension
    {
        public static Boolean TryGet(this BindingRowState value, out IRowStateEnumeration result)
        {
            if (Enumeration.TryGet(value, out Enumeration enumValue))
            { result = enumValue; return true; }
            else { result = enumValue; return false; }
        }

        static void test()
        {
            String value = String.Empty;
            value = TryGet(BindingRowState.Added, out IRowStateEnumeration result) ? result.Name: value;
        }

        /// <summary>
        /// Returns the BindingRowState of a BindingSource
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public static BindingRowState GetRowState(this BindingSource binding)
        {
            BindingRowState result = BindingRowState.Null;

            if (binding.Position >= binding.Count)
            { binding.Position = 0; }

            if (binding.Position >= 0
                && binding.Current is IBindingRowState rowState
                && result is BindingRowState.Null or BindingRowState.Unchanged)
            { result = rowState.RowState().AsBindingRowState(); }

            if (binding.Position >= 0
                && binding.Current is ITemporal temporal
                && temporal.Temporal.IsCurrent == false
                && result is BindingRowState.Null or BindingRowState.Unchanged)
            { return BindingRowState.Historic; }

            return result;
        }
    }
}
