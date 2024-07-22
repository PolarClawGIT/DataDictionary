using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Schema
{
    /// <summary>
    /// Interface for the Database Schema Key.
    /// </summary>
    public interface IDbSchemaKey : IKey
    {
        /// <summary>
        /// Application ID for the Schema.
        /// </summary>
        Guid? SchemaId { get; }
    }

    /// <summary>
    /// Implementation for the Database Schema Key.
    /// </summary>
    public class DbSchemaKey : IDbSchemaKey, IKeyEquality<IDbSchemaKey>
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Schema Key.
        /// </summary>
        /// <param name="source"></param>
        public DbSchemaKey(IDbSchemaKey source) : base()
        {
            if (source.SchemaId is Guid value) { SchemaId = value; }
            else { SchemaId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbSchemaKey? other)
        { return other is IDbSchemaKey && EqualityComparer<Guid?>.Default.Equals(SchemaId, other.SchemaId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbSchemaKey value && Equals(new DbSchemaKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbSchemaKey left, DbSchemaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbSchemaKey left, DbSchemaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(SchemaId); }
        #endregion
    }
}
