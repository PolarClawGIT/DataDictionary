using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Generic Base class for Database Extended Property Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbExtendedPropertyCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDbCatalogKey>,
        IWriteData<IModelKey>, IWriteData<IDbCatalogKey>,
        IRemoveItem<IDbCatalogKey>
        where TItem : BindingTableRow, IDbExtendedPropertyItem, IDbCatalogKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, Guid? propertyId, string? catalogName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@PropertyId", parameters.propertyId);
            command.AddParameter("@CatalogName", parameters.catalogName);
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
            command.CommandText = "[App_DataDictionary].[procSetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseExtendedProperty]", this);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDbCatalogKey catalogItem)
        {
            DbCatalogKey key = new DbCatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// List of Database Extended Property Items.
    /// </summary>
    public class DbExtendedPropertyCollection : DbExtendedPropertyCollection<DbExtendedPropertyItem>
    { }
}
