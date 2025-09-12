namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on Scope Enum. 
    /// </summary>
    public static class ScopeExtension
    {
        /// <summary>
        /// Gets the Details for the Scope enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IScopeEnumeration GetEnumeration(this ScopeType value)
        { return ScopeEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a Scope enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out ScopeType result)
        {
            if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = ScopeType.Null; return false; }
        }
    }
}
