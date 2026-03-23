namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the TemplateObject database operations.
    /// </summary>
    static class TemplateObject
    {
        static readonly DatabaseNaming schema = new DatabaseNaming(typeof(TemplateObject));

        /// <summary>
        /// Stored procedure to retrieve an TemplateObject.
        /// </summary>
        public readonly static String GetProcedure = schema.GetProcedure;

        /// <summary>
        /// Stored procedure to set an TemplateObject.
        /// </summary>
        public readonly static String SetProcedure = schema.SetProcedure;

        /// <summary>
        /// User-defined table type for TemplateObject operations.
        /// </summary>
        public readonly static String TableType = schema.TableType;
    }
}
