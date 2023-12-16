using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Column Key.
    /// </summary>
    public interface IDbTableColumnKey : IKey
    {
        /// <summary>
        /// Application ID for the Table Column.
        /// </summary>
        Guid? ColumnId { get; }
    }

    /// <summary>
    /// Implementation for the Database Table Column Key.
    /// </summary>
    public class DbTableColumnKey : IDbTableColumnKey, IKeyEquality<IDbTableColumnKey>
    {
        /// <inheritdoc/>
        public Guid? ColumnId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the TableColumn Key.
        /// </summary>
        /// <param name="source"></param>
        public DbTableColumnKey(IDbTableColumnKey source) : base()
        {
            if (source.ColumnId is Guid value) { ColumnId = value; }
            else { ColumnId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbTableColumnKey? other)
        { return other is IDbTableColumnKey && EqualityComparer<Guid?>.Default.Equals(ColumnId, other.ColumnId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbTableColumnKey value && Equals(new DbTableColumnKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbTableColumnKey left, DbTableColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableColumnKey left, DbTableColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(ColumnId); }
        #endregion
    }
}
