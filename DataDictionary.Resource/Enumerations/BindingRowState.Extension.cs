using System.Data;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on NodeRenderAs Enum. 
    /// </summary>
    public static class BindingRowStateExtension
    {
        /// <summary>
        /// Allows conversion from a DataRowState to a BindingRowState
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BindingRowState AsBindingRowState(this DataRowState source)
        { return (BindingRowState)((Int32)source); }

        public static DataRowState AsDataRowState(this BindingRowState source)
        {
            // This should throw an exception.
            return Enum.GetValues<DataRowState>().FirstOrDefault(w => ((Int32)w).Equals((Int32)source));
        }

        /// <summary>
        /// Gets the Details for the BindingRowState enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BindingRowStateEnumeration GetEnumeration(this BindingRowState value)
        { return BindingRowStateEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a BindingRowState enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out BindingRowState result)
        {
            if (BindingRowStateEnumeration.TryParse(value, null, out BindingRowStateEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = BindingRowState.Null; return false; }
        }
    }
}
