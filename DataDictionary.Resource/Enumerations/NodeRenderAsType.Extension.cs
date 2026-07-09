namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on NodeRenderAs Enum. 
    /// </summary>
    public static class NodeRenderAsExtension
    {
        /// <summary>
        /// Gets the Details for the NodeRenderAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<NodeRenderAsType> GetEnumeration(this NodeRenderAsType value)
        { return NodeRenderAsEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a NodeRenderAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out NodeRenderAsType result)
        {
            if (NodeRenderAsEnumeration.TryParse(value, null, out NodeRenderAsEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = NodeRenderAsType.none; return false; }
        }
    }
}
