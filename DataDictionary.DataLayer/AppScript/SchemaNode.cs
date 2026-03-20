namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the SchemaNode database operations.
    /// </summary>
    static class SchemaNode
    {
        static readonly DatabaseNaming schema = new DatabaseNaming(typeof(SchemaNode));

        /// <summary>
        /// Stored procedure to retrieve an SchemaNode.
        /// </summary>
        public readonly static String GetProcedure = schema.GetProcedure;

        /// <summary>
        /// Stored procedure to set an SchemaNode.
        /// </summary>
        public readonly static String SetProcedure = schema.SetProcedure;

        /// <summary>
        /// User-defined table type for SchemaNode operations.
        /// </summary>
        public readonly static String TableType = schema.TableType;
    }
}
