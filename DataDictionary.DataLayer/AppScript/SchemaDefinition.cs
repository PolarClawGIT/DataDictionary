namespace DataDictionary.DataLayer.AppScript
{

    /// <summary>
    /// Static class containing constants related to the SchemaDefinition database operations.
    /// </summary>
    static class SchemaDefinition
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(SchemaDefinition));

        /// <summary>
        /// Stored procedure to retrieve an SchemaDefinition.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set an SchemaDefinition.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// User-defined table type for SchemaDefinition operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }

}
