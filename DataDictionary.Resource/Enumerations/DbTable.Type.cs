namespace DataDictionary.Resource.Enumerations
{
    // This is all in one file because they are so closely related and was easer to find.

    /// <summary>
    /// List of supported Table Types.
    /// </summary>
    public enum DbTableType
    {
        /// <summary>
        /// Unknown Table Type
        /// </summary>
        Null,

        /// <summary>
        /// Base Table
        /// </summary>
        Table,

        /// <summary>
        /// Temporal Table
        /// </summary>
        TemporalTable,

        /// <summary>
        /// History Table, backed by Temporal Table
        /// </summary>
        HistoryTable,

        /// <summary>
        /// View
        /// </summary>
        View
    }
}
