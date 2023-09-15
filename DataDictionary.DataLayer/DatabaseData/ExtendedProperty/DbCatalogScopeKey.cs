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
    /// <see cref="https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16"/>
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
    public interface IDbCatalogScope
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
    public class DbCatalogScopeKey : IDbCatalogScope
    {
        /// <inheritdoc/>
        public DbCatalogScope CatalogScope { get; init; } = DbCatalogScope.NULL;
    }
}
