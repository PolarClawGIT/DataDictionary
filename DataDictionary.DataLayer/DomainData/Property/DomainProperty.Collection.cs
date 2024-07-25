using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Property
{
    /// <summary>
    /// Generic Base class for Domain Properties
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainPropertyCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IDomainPropertyKey>,
        IWriteData<IModelKey>, IWriteData<IDomainPropertyKey>,
        IDeleteData<IDomainPropertyKey>,
        IRemoveItem<IDomainPropertyKey>
        where TItem : BindingTableRow, IDomainPropertyItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainPropertyKey propertyKey)
        { return LoadCommand(connection, (null, propertyKey.PropertyId, null)); }

        Command LoadCommand(IConnection connection, (Guid? ModelId, Guid? PropertyId, string? PropertyTitle) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainProperty]";
            command.AddParameter("@ModelId", parameters.ModelId);
            command.AddParameter("@PropertyId", parameters.PropertyId);
            command.AddParameter("@PropertyTitle", parameters.PropertyTitle);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, (modelId.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDomainPropertyKey PropertyKey)
        { return SaveCommand(connection, (null, PropertyKey.PropertyId, null)); }

        Command SaveCommand(IConnection connection, (Guid? ModelId, Guid? PropertyId, String? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainProperty]";
            command.AddParameter("@ModelId", parameters.ModelId);
            command.AddParameter("@PropertyId", parameters.PropertyId);

            IEnumerable<TItem> data = this.Where(w => parameters.PropertyId is null || w.PropertyId == parameters.PropertyId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainProperty]", data);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IDomainPropertyKey parameters)
        { return DeleteCommand(connection, (parameters.PropertyId, null)); }

        Command DeleteCommand(IConnection connection, (Guid? PropertyId, String? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainProperty]";
            command.AddParameter("@PropertyId", parameters.PropertyId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDomainPropertyKey PropertyItem)
        {
            DomainPropertyKey key = new DomainPropertyKey(PropertyItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
