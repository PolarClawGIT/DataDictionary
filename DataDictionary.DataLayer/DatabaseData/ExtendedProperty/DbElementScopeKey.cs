using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Level2 MS Extended Property Types. These are Element Level.
    /// Not all types are supported by the Application.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
    public enum DbElementScope
    {
        /// <summary>
        /// Not defined, default value.
        /// </summary>
        NULL,

        /// <summary>
        /// MS SQL Default.
        /// </summary>
        Default,

        /// <summary>
        /// MS SQL Column. Application Supported.
        /// </summary>
        Column,

        /// <summary>
        /// MS SQL Constraint.
        /// </summary>
        Constraint,

        /// <summary>
        /// MS SQL EventNotification.
        /// </summary>
        EventNotification,

        /// <summary>
        /// MS SQL Index.
        /// </summary>
        Index,

        /// <summary>
        /// MS SQL Parameter. Application Supported.
        /// </summary>
        Parameter,

        /// <summary>
        /// MS SQL Trigger.
        /// </summary>
        Trigger,
    }

    /// <summary>
    /// Interface for Level2 MS Extended Property Type.
    /// </summary>
    public interface IDbElementScopeKey: IDbObjectScopeKey
    {
        /// <summary>
        /// Level2 MS Extended Property Type.
        /// </summary>
        public DbElementScope ElementScope { get; }
    }

    /// <summary>
    /// Implementation of the Key for Level2 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbElementScopeKey : DbObjectScopeKey, IDbElementScopeKey, IKeyEquality<IDbElementScopeKey>
    {
        /// <inheritdoc/>
        public DbElementScope ElementScope { get; init; } = DbElementScope.NULL;

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        internal protected DbElementScopeKey() : base() { }

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        public DbElementScopeKey(IDbElementScopeKey source) : base(source)
        { ElementScope = source.ElementScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbElementScopeKey? other)
        {
            return
                other is IDbObjectScopeKey
                && new DbObjectScopeKey(this).Equals(other)
                && ObjectScope != DbObjectScope.NULL
                && other.ObjectScope != DbObjectScope.NULL
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbElementScopeKey value && Equals(new DbElementScopeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbElementScopeKey left, DbElementScopeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbElementScopeKey left, DbElementScopeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ElementScope); }
        #endregion

    }
}
