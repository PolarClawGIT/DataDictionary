namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the Transform database operations.
    /// </summary>
    static class Transform
    {
        static readonly DatabaseNaming schema = new DatabaseNaming(typeof(Transform));

        /// <summary>
        /// Stored procedure to retrieve an Transform.
        /// </summary>
        public readonly static String GetProcedure = schema.GetProcedure;

        /// <summary>
        /// Stored procedure to set an Transform.
        /// </summary>
        public readonly static String SetProcedure = schema.SetProcedure;

        /// <summary>
        /// User-defined table type for Transform operations.
        /// </summary>
        public readonly static String TableType = schema.TableType;
    }
}
