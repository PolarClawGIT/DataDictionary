using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.ModelData;
using Microsoft.Data.SqlClient;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Domain Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadSchema<ICatalogKey>,
        IWriteData<IModelKey>, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IDomainKeyName>
        where TItem : BindingTableRow, IDbDomainItem, ICatalogKey, IDomainKeyName, new()
    {
        /// <inheritdoc/>
        [Obsolete("Needs work")]
        public Command SchemaCommand(IConnection connection, ICatalogKey catalogKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbDomainItem;
            command.Parameters.Add(new SqlParameter(SqlScript.Catalog.CatalogId, SqlDbType.UniqueIdentifier) { Value = catalogKey.CatalogId });
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        Command LoadCommand(IConnection connection, Guid? modelId = null, Guid? catalogId = null, Guid? domainId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SqlScript.Domain.GetMethod;
            command.AddParameter(SqlScript.Model.ModelId, modelId);
            command.AddParameter(SqlScript.Catalog.CatalogId, catalogId);
            command.AddParameter(SqlScript.Domain.DomainId, domainId);

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, catalogId: catalogKey.CatalogId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? catalogId = null, Guid? domainId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SqlScript.Domain.SetMethod;
            command.AddParameter(SqlScript.Model.ModelId, modelId);
            command.AddParameter(SqlScript.Catalog.CatalogId, catalogId);
            command.AddParameter(SqlScript.Domain.DomainId, domainId);

            IEnumerable<TItem> data = this.Where(w => catalogId is null || w.CatalogId == catalogId);
            command.AddParameter(SqlScript.Common.Data, SqlScript.Domain.TableType, data);
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
        public virtual void Remove(IDomainKeyName domainItem)
        {
            DomainKeyName key = new DomainKeyName(domainItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
