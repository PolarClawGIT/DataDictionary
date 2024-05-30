using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Definition
{
    /// <summary>
    /// Generic Base class for Domain Definitions
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainDefinitionCollection<TItem> : BindingTable<TItem>,
        IReadData<IDomainDefinitionKey>,
        IWriteData<IDomainDefinitionKey>,
        IDeleteData<IDomainDefinitionKey>,
        IRemoveItem<IDomainDefinitionKey>
        where TItem : BindingTableRow, IDomainDefinitionItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainDefinitionKey DefinitionKey)
        { return LoadCommand(connection, (DefinitionKey.DefinitionId, null)); }

        Command LoadCommand(IConnection connection, (Guid? DefinitionId, string? DefinitionTitle) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainDefinition]";
            command.AddParameter("@DefinitionId", parameters.DefinitionId);
            command.AddParameter("@DefinitionTitle", parameters.DefinitionTitle);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDomainDefinitionKey DefinitionKey)
        { return SaveCommand(connection, (DefinitionKey.DefinitionId, null)); }

        Command SaveCommand(IConnection connection, (Guid? DefinitionId, String? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainDefinition]";
            command.AddParameter("@DefinitionId", parameters.DefinitionId);

            IEnumerable<TItem> data = this.Where(w => parameters.DefinitionId is null || w.DefinitionId == parameters.DefinitionId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainDefinition]", data);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IDomainDefinitionKey parameters)
        { return DeleteCommand(connection, (parameters.DefinitionId, null)); }

        Command DeleteCommand(IConnection connection, (Guid? DefinitionId, String? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainDefinition]";
            command.AddParameter("@DefinitionId", parameters.DefinitionId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDomainDefinitionKey DefinitionItem)
        {
            DomainDefinitionKey key = new DomainDefinitionKey(DefinitionItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
