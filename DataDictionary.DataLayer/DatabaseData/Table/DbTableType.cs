namespace DataDictionary.DataLayer.DatabaseData.Table
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

    /// <summary>
    /// Interface for Database TableType Key.
    /// </summary>
    public interface IDbTableType : IKey
    {
        /// <summary>
        /// Type of Table (Table, Temporal Table, Historic Table, View)
        /// </summary>
        DbTableType TableType { get; }
    }
}
