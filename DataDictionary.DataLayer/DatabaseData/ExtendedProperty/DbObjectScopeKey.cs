using DataDictionary.DataLayer.ApplicationData.Scope;
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
    public enum DbObjectScope
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        NULL,

        /// <summary>
        /// MS SQL Aggregate type.
        /// </summary>
        Aggregate,

        /// <summary>
        /// MS SQL Default type. Application Supported?
        /// </summary>
        Default,

        /// <summary>
        /// MS SQL Function type. Application Supported.
        /// </summary>
        Function,

        /// <summary>
        /// MS SQL LogicalFileName type.
        /// </summary>
        LogicalFileName,

        /// <summary>
        /// MS SQL Procedure type. Application Supported.
        /// </summary>
        Procedure,

        /// <summary>
        /// MS SQL Queue type.
        /// </summary>
        Queue,

        /// <summary>
        /// MS SQL Rule type.
        /// </summary>
        Rule,

        /// <summary>
        /// MS SQL Synonym type.
        /// </summary>
        Synonym,

        /// <summary>
        /// MS SQL Table type. Application Supported.
        /// </summary>
        Table,

        /// <summary>
        /// MS SQL Type type. Application Supported.
        /// </summary>
        Type,

        /// <summary>
        /// MS SQL View type. Application Supported.
        /// </summary>
        View,

        /// <summary>
        /// MS SQL XmlSchemaCollection type.
        /// </summary>
        XmlSchemaCollection,
    }

    /// <summary>
    /// Interface for Level1 MS Extended Property Type.
    /// </summary>
    public interface IDbObjectScopeKey: IDbCatalogScopeKey
    {
        /// <summary>
        /// Level1 MS Extended Property Type.
        /// </summary>
        public DbObjectScope ObjectScope { get; }
    }

    /// <summary>
    /// Implementation of the Key for Level1 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbObjectScopeKey : DbCatalogScopeKey, IDbObjectScopeKey, IKeyEquality<IDbObjectScopeKey>
    {
        /// <inheritdoc/>
        public DbObjectScope ObjectScope { get; init; } = DbObjectScope.NULL;

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        internal protected DbObjectScopeKey() : base() { }

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        public DbObjectScopeKey(IDbObjectScopeKey source) : base (source)
        { ObjectScope = source.ObjectScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbObjectScopeKey? other)
        {
            return
                other is IDbObjectScopeKey
                && new DbCatalogScopeKey(this).Equals(other)
                && ObjectScope != DbObjectScope.NULL
                && other.ObjectScope != DbObjectScope.NULL
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbObjectScopeKey value && Equals(new DbObjectScopeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbObjectScopeKey left, DbObjectScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbObjectScopeKey left, DbObjectScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectScope); }
        #endregion
    }
}
