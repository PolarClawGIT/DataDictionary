namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the Document database operations.
    /// </summary>
    static class TransformDocument
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TransformDocument));

        /// <summary>
        /// Stored procedure to retrieve an Document.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set an Document.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// User-defined table type for Document operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
