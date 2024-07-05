using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Level1 MS Extended Property Types. These are Object Level.
    /// Not all types are supported by the Application.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
    public enum DbLevelObject
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        NULL,

        /// <summary>
        /// MS SQL Aggregate.
        /// </summary>
        Aggregate,

        /// <summary>
        /// MS SQL Default.
        /// </summary>
        Default,

        /// <summary>
        /// MS SQL Function. Application Supported.
        /// </summary>
        Function,

        /// <summary>
        /// MS SQL LogicalFileName.
        /// </summary>
        LogicalFileName,

        /// <summary>
        /// MS SQL Procedure. Application Supported.
        /// </summary>
        Procedure,

        /// <summary>
        /// MS SQL Queue.
        /// </summary>
        Queue,

        /// <summary>
        /// MS SQL Rule.
        /// </summary>
        Rule,

        /// <summary>
        /// MS SQL Synonym.
        /// </summary>
        Synonym,

        /// <summary>
        /// MS SQL Table. Application Supported.
        /// </summary>
        Table,

        /// <summary>
        /// MS SQL Type. Application Supported.
        /// </summary>
        Type,

        /// <summary>
        /// MS SQL View. Application Supported.
        /// </summary>
        View,

        /// <summary>
        /// MS SQL XmlSchemaCollection.
        /// </summary>
        XmlSchemaCollection,
    }

    /// <summary>
    /// Interface for Level1 MS Extended Property Type.
    /// </summary>
    public interface IDbLevelObjectKey: IDbLevelCatalogKey
    {
        /// <summary>
        /// Level1 MS Extended Property Type.
        /// </summary>
        public DbLevelObject ObjectScope { get; }
    }

    /// <summary>
    /// Implementation of the Key for Level1 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbLevelObjectKey : DbLevelCatalogKey, IDbLevelObjectKey, IKeyEquality<IDbLevelObjectKey>
    {
        /// <inheritdoc/>
        public DbLevelObject ObjectScope { get; init; } = DbLevelObject.NULL;

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        internal protected DbLevelObjectKey() : base() { }

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        public DbLevelObjectKey(IDbLevelObjectKey source) : base (source)
        { ObjectScope = source.ObjectScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbLevelObjectKey? other)
        {
            return
                other is IDbLevelObjectKey
                && new DbLevelCatalogKey(this).Equals(other)
                && ObjectScope != DbLevelObject.NULL
                && other.ObjectScope != DbLevelObject.NULL
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbLevelObjectKey value && Equals(new DbLevelObjectKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbLevelObjectKey left, DbLevelObjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbLevelObjectKey left, DbLevelObjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectScope); }
        #endregion
    }
}
