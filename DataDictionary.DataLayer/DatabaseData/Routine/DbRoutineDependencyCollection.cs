using DataDictionary.DataLayer.ApplicationData.Model;
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
    public abstract class DbRoutineDependencyCollection<TItem> : BindingTable<TItem>, IReadData<IModelKey>, IReadSchema<IDbRoutineKey>, IWriteData<IModelKey>
        where TItem : DbRoutineDependencyItem, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, IDbRoutineKey key)
        {
            Command result = connection.CreateCommand();
            result.CommandType = CommandType.Text;
            result.CommandText = DbScript.DbRoutineDependencyItem;
            result.Parameters.Add(new SqlParameter("@ObjectName", SqlDbType.VarChar, 210));
            result.Parameters["@ObjectName"].Value = string.Format("[{0}].[{1}]", key.SchemaName, key.RoutineName);
            return result;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, string? catalogName, string? schemaName, string? routineName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseRoutineDependency]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@RoutineName", parameters.routineName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseRoutineDependency]";
            command.AddParameter("@ModelId", modelId.ModelId);
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
