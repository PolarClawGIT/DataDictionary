namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the Template database operations.
    /// </summary>
    static class Template
    {
        static readonly DatabaseNaming schema = new DatabaseNaming(typeof(Template));

        /// <summary>
        /// Identifier for the Template.
        /// </summary>
        public readonly static String Identifier = schema.Identifier;

        /// <summary>
        /// Stored procedure to retrieve an Template.
        /// </summary>
        public readonly static String GetProcedure = schema.GetProcedure;

        /// <summary>
        /// Stored procedure to set an Template.
        /// </summary>
        public readonly static String SetProcedure = schema.SetProcedure;

        /// <summary>
        /// User-defined table type for Template operations.
        /// </summary>
        public readonly static String TableType = schema.TableType;
    }
}
