// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for DbTableType
    /// </summary>
    public interface IDbTableType
    {
        /// <summary>
        /// Type of Table (Table, Temporal Table, Historic Table, View)
        /// </summary>
        DbTableType TableType { get; }
    }

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
