namespace DataDictionary.DataLayer.AppScript
{
    static class SchemaNodeOwner
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(SchemaNodeOwner));

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
