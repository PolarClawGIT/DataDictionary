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
        /// MS SQL Default type.
        /// </summary>
        Default,

        /// <summary>
        /// MS SQL Column type. Application Supported.
        /// </summary>
        Column,

        /// <summary>
        /// MS SQL Constraint type.
        /// </summary>
        Constraint,

        /// <summary>
        /// MS SQL EventNotification type.
        /// </summary>
        EventNotification,

        /// <summary>
        /// MS SQL Index type.
        /// </summary>
        Index,

        /// <summary>
        /// MS SQL Parameter type. Application Supported.
        /// </summary>
        Parameter,

        /// <summary>
        /// MS SQL Trigger type.
        /// </summary>
        Trigger,
    }

    /// <summary>
    /// Interface for Level2 MS Extended Property Type.
    /// </summary>
    public interface IDbElementScope
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
    public class DbElementScopeKey : DbObjectScopeKey, IDbElementScope
    {
        /// <inheritdoc/>
        public DbElementScope ElementScope { get; init; } = DbElementScope.NULL;
    }
}
