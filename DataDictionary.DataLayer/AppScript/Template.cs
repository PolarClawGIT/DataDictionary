namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the Template database operations.
    /// </summary>
    static class Template
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Template));

        /// <summary>
        /// Identifier for the Template.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// Stored procedure to retrieve an Template.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set an Template.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// User-defined table type for Template operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
