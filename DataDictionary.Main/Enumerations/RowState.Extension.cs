using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Enumerations
{
    static partial class RowStateExtension
    {
        /// <summary>
        /// Try/Get the RowState information for the BindingRowState enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetValue(this BindingRowState value, [NotNullWhen(true)] out IRowStateEnumeration? result)
        {
            if (Enumeration.TryGetValue(value, out Enumeration? enumValue))
            { result = enumValue; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Gets the Image for the BindingRowState
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">BindingRowState is not in RowState Enumeration</exception>
        public static Image GetImage(this BindingRowState value)
        { return Enumeration.GetValue(value).Image; }

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
