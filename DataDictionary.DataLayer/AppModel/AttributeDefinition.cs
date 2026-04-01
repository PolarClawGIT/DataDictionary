namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Static class containing constants related to the Attribute Definition database operations.
    /// </summary>
    static class AttributeDefinition
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(AttributeDefinition));

        /// <summary>
        /// The stored procedure used to retrieve attribute definitions.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// The stored procedure used to set attribute definitions.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// The user-defined table type for attribute definitions.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}