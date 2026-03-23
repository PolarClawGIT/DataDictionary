namespace DataDictionary.DataLayer.AppScript
{
    static class SchemaNodeOwner
    {
        static readonly DatabaseNaming schema = new DatabaseNaming(typeof(SchemaNodeOwner));

        /// <summary>
        /// Stored procedure to retrieve an SchemaDefinition.
        /// </summary>
        public readonly static String GetProcedure = schema.GetProcedure;

        /// <summary>
        /// Stored procedure to set an SchemaDefinition.
        /// </summary>
        public readonly static String SetProcedure = schema.SetProcedure;

        /// <summary>
        /// User-defined table type for SchemaDefinition operations.
        /// </summary>
        public readonly static String TableType = schema.TableType;
    }
}
