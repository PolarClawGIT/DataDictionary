namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the TemplateObject database operations.
    /// </summary>
    static class TemplateObject
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TemplateObject));

        /// <summary>
        /// Stored procedure to retrieve an TemplateObject.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set an TemplateObject.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// User-defined table type for TemplateObject operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
