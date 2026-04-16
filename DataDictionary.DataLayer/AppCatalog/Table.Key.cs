using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Table Key.
    /// </summary>
    public interface ITableKey : IKey
    {
        /// <summary>
        /// Application ID for the Table.
        /// </summary>
        Guid? TableId { get; }
    }

    /// <summary>
    /// Implementation for the Database Table Key.
    /// </summary>
    public class TableKey : ITableKey,
        IKeyEquality<ITableKey>, IKeyEquality<TableKey>
    {
        /// <inheritdoc/>
        public Guid? TableId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return TableId.HasValue && TableId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Table Key.
        /// </summary>
        /// <param name="source"></param>
        public TableKey(ITableKey source) : base()
        {
            if (source.TableId is Guid value) { TableId = value; }
            else { TableId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TableKey? other)
        {
            return other is TableKey key
                && TableId.HasValue && TableId != Guid.Empty
                && key.TableId.HasValue && key.TableId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(TableId, other.TableId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ITableKey? other)
        { return other is ITableKey value && Equals(new TableKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ITableKey value && Equals(new TableKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TableKey left, TableKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TableKey left, TableKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(TableId); }
        #endregion
    }
}
