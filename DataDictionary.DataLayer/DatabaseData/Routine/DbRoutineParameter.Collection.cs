using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.ModelData;
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
    /// Generic Base class for Database Routine Parameter Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbRoutineParameterCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadSchema<ICatalogKey>,
        IWriteData<IModelKey>, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IDbSchemaKeyName>, IRemoveItem<IDbRoutineKeyName>, IRemoveItem<IDbRoutineParameterKeyName>
        where TItem : BindingTableRow, IDbRoutineParameterItem, ICatalogKey, IDbSchemaKeyName, IDbRoutineKeyName, IDbRoutineParameterKeyName, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, ICatalogKey catalogKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbRoutineParameterItem;
            command.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = catalogKey.CatalogId });
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, string? catalogName, string? schemaName, string? routineName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseRoutineParameter]";
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
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, (null, catalogKey.CatalogId)); }

        /// <inheritdoc/>
        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? catalogId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseRoutineParameter]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);

            IEnumerable<TItem> data = this.Where(w => parameters.catalogId is null || w.CatalogId == parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseRoutineParameter]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ICatalogKey catalogItem)
        {
            CatalogKey key = new CatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IDbRoutineKeyName routineItem)
        {
            DbRoutineKeyName key = new DbRoutineKeyName(routineItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IDbRoutineParameterKeyName parameterItem)
        {
            DbRoutineParameterKeyName key = new DbRoutineParameterKeyName(parameterItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IDbSchemaKeyName schemaItem)
        {
            DbSchemaKeyName key = new DbSchemaKeyName(schemaItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Routine Parameter Items
    /// </summary>
    public class DbRoutineParameterCollection : DbRoutineParameterCollection<DbRoutineParameterItem>
    { }
}
