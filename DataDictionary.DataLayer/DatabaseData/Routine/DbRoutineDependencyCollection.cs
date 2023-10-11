using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Generic Base class for Database Catalogs Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbRoutineDependencyCollection<TItem> : BindingTable<TItem>, IReadData<IModelKey>, IReadData<IDbCatalogKey>, IReadSchema<IDbRoutineItem>, IWriteData<IModelKey>, IWriteData<IDbCatalogKey>
        where TItem : DbRoutineDependencyItem, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, IDbRoutineItem key)
        {
            Command result = connection.CreateCommand();
            result.CommandType = CommandType.Text;
            result.CommandText = DbScript.DbRoutineDependencyItem;
            result.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = key.CatalogId });
            result.Parameters.Add(new SqlParameter("@ObjectName", SqlDbType.VarChar, 210));
            result.Parameters["@ObjectName"].Value = string.Format("[{0}].[{1}]", key.SchemaName, key.RoutineName);
            return result;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, string? catalogName, string? schemaName, string? routineName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseRoutineDependency]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@RoutineName", parameters.routineName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, (modelKey.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return SaveCommand(connection, (null, catalogKey.CatalogId)); }

        /// <inheritdoc/>
        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? catalogId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseRoutineDependency]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseRoutineDependency]", this);
            return command;
        }
    }

    /// <summary>
    /// Default List/Collection of Database Routine Dependencies
    /// </summary>
    public class DbRoutineDependencyCollection : DbRoutineDependencyCollection<DbRoutineDependencyItem>
    { }
}
