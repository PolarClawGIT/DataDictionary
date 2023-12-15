using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Level0 MS Extended Property Types. These are Database Level.
    /// Not all types are supported by the Application.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
    public enum DbCatalogScope
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        NULL,

        /// <summary>
        /// MS SQL Assembly type.
        /// </summary>
        Assembly,

        /// <summary>
        /// MS SQL Contract type.
        /// </summary>
        Contract,

        /// <summary>
        /// MS SQL EventNotification type.
        /// </summary>
        EventNotification,

        /// <summary>
        /// MS SQL FileGroup type.
        /// </summary>
        Filegroup,

        /// <summary>
        /// MS SQL MessageType type.
        /// </summary>
        MessageType,

        /// <summary>
        /// MS SQL PartitionFunction type.
        /// </summary>
        PartitionFunction,

        /// <summary>
        /// MS SQL PartitionScheme type.
        /// </summary>
        PartitionScheme,

        /// <summary>
        /// MS SQL RemoteServiceBinding type.
        /// </summary>
        RemoteServiceBinding,

        /// <summary>
        /// MS SQL Route type.
        /// </summary>
        Route,

        /// <summary>
        /// MS SQL Schema type. Application Supported.
        /// </summary>
        Schema,

        /// <summary>
        /// MS SQL Service type.
        /// </summary>
        Service,

        /// <summary>
        /// MS SQL Trigger type.
        /// </summary>
        Trigger,

        /// <summary>
        /// MS SQL Type type.
        /// </summary>
        Type,

        /// <summary>
        /// MS SQL User type.
        /// </summary>
        User,
    }

    /// <summary>
    /// Interface for Level0 MS Extended Property Type.
    /// </summary>
    public interface IDbCatalogScopeKey : IDbScopeKey
    {
        /// <summary>
        /// Level0 MS Extended Property Type.
        /// </summary>
        DbCatalogScope CatalogScope { get; }
    }

    /// <summary>
    /// Implementation of the Key for Level0 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbCatalogScopeKey : IDbCatalogScopeKey, IKeyEquality<IDbCatalogScopeKey>
    {
        /// <inheritdoc/>
        public DbCatalogScope CatalogScope { get; init; } = DbCatalogScope.NULL;

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        internal protected DbCatalogScopeKey() : base() { }

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        public DbCatalogScopeKey(IDbCatalogScopeKey source) : this()
        { CatalogScope = source.CatalogScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbCatalogScopeKey? other)
        {
            return
                other is IDbCatalogScopeKey
                && CatalogScope != DbCatalogScope.NULL
                && other.CatalogScope != DbCatalogScope.NULL
                && CatalogScope == other.CatalogScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbCatalogScopeKey value && Equals(new DbCatalogScopeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbCatalogScopeKey left, DbCatalogScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbCatalogScopeKey left, DbCatalogScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogScope); }
        #endregion
    }
}
