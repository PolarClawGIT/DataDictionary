using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Key.
    /// </summary>
    public interface IDbTableKey : IKey
    {
        /// <summary>
        /// Application ID for the Table.
        /// </summary>
        Guid? TableId { get; }
    }

    /// <summary>
    /// Implementation for the Database Table Key.
    /// </summary>
    public class DbTableKey : IDbTableKey, IKeyEquality<IDbTableKey>
    {
        /// <inheritdoc/>
        public Guid? TableId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Table Key.
        /// </summary>
        /// <param name="source"></param>
        public DbTableKey(IDbTableKey source) : base()
        {
            if (source.TableId is Guid value) { TableId = value; }
            else { TableId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbTableKey? other)
        { return other is IDbTableKey && EqualityComparer<Guid?>.Default.Equals(TableId, other.TableId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbTableKey value && Equals(new DbTableKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbTableKey left, DbTableKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableKey left, DbTableKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(TableId); }
        #endregion
    }
}
