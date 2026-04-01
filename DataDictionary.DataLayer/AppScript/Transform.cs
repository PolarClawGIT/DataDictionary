namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the Transform database operations.
    /// </summary>
    static class Transform
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Transform));

        /// <summary>
        /// Stored procedure to retrieve an Transform.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set an Transform.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// User-defined table type for Transform operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
