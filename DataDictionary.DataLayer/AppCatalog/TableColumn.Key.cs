using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Table Column Key.
    /// </summary>
    public interface ITableColumnKey : IKey
    {
        /// <summary>
        /// Application ID for the Table Column.
        /// </summary>
        Guid? ColumnId { get; }
    }

    /// <summary>
    /// Implementation for the Database Table Column Key.
    /// </summary>
    public class TableColumnKey : ITableColumnKey,
        IKeyEquality<ITableColumnKey>, IKeyEquality<TableColumnKey>
    {
        /// <inheritdoc/>
        public Guid? ColumnId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the TableColumn Key.
        /// </summary>
        /// <param name="source"></param>
        public TableColumnKey(ITableColumnKey source) : base()
        {
            if (source.ColumnId is Guid value) { ColumnId = value; }
            else { ColumnId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TableColumnKey? other)
        { return other is TableColumnKey && EqualityComparer<Guid?>.Default.Equals(ColumnId, other.ColumnId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(ITableColumnKey? other)
        { return other is ITableColumnKey value && Equals(new TableColumnKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ITableColumnKey value && Equals(new TableColumnKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TableColumnKey left, TableColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TableColumnKey left, TableColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ColumnId); }
        #endregion
    }
}
