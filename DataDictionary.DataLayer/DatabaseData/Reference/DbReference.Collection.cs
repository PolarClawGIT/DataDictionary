﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
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

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Generic Base class for Database Reference Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class DbReferenceCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDbCatalogKey>, 
        IWriteData<IModelKey>, IWriteData<IDbCatalogKey>,
        IReadSchema<IDbRoutineItem>, IReadSchema<IDbTableItem>,
        IRemoveItem<IDbCatalogKey>
        where TItem : BindingTableRow, IDbReferenceItem, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, IDbRoutineItem key)
        {
            Command result = connection.CreateCommand();
            result.CommandType = CommandType.Text;
            result.CommandText = DbScript.DbReferenceItem;
            result.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = key.CatalogId });
            result.Parameters.Add(new SqlParameter("@ObjectName", SqlDbType.VarChar, 210));
            result.Parameters["@ObjectName"].Value = string.Format("[{0}].[{1}]", key.SchemaName, key.RoutineName);
            return result;
        }

        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, IDbTableItem key)
        {
            Command result = connection.CreateCommand();
            result.CommandType = CommandType.Text;
            result.CommandText = DbScript.DbReferenceItem;
            result.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = key.CatalogId });
            result.Parameters.Add(new SqlParameter("@ObjectName", SqlDbType.VarChar, 210));
            result.Parameters["@ObjectName"].Value = string.Format("[{0}].[{1}]", key.SchemaName, key.TableName);
            return result;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, String? catalogName, String? schemaName, String? objectName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseReference]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ObjectName", parameters.objectName);
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
            command.CommandText = "[App_DataDictionary].[procSetDatabaseReference]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);

            IEnumerable<TItem> data = this.Where(w => parameters.catalogId is null || w.CatalogId == parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseReference]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDbCatalogKey catalogItem)
        {
            DbCatalogKey key = new DbCatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }


        /// <inheritdoc/>
        public virtual void Remove(IDbReferenceKeyName dependencyItem)
        {
            DbReferenceKeyName key = new DbReferenceKeyName(dependencyItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

}
