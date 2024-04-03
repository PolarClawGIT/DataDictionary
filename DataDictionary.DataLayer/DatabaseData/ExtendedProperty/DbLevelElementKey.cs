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
    public enum DbLevelElement
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
    public interface IDbLevelElementKey: IDbLevelObjectKey
    {
        /// <summary>
        /// Level2 MS Extended Property Type.
        /// </summary>
        public DbLevelElement ElementScope { get; }
    }

    /// <summary>
    /// Implementation of the Key for Level2 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbLevelElementKey : DbLevelObjectKey, IDbLevelElementKey, IKeyEquality<IDbLevelElementKey>
    {
        /// <inheritdoc/>
        public DbLevelElement ElementScope { get; init; } = DbLevelElement.NULL;

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        internal protected DbLevelElementKey() : base() { }

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        public DbLevelElementKey(IDbLevelElementKey source) : base(source)
        { ElementScope = source.ElementScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbLevelElementKey? other)
        {
            return
                other is IDbLevelObjectKey
                && new DbLevelObjectKey(this).Equals(other)
                && ObjectScope != DbLevelObject.NULL
                && other.ObjectScope != DbLevelObject.NULL
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbLevelElementKey value && Equals(new DbLevelElementKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbLevelElementKey left, DbLevelElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbLevelElementKey left, DbLevelElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ElementScope); }
        #endregion

    }
}
