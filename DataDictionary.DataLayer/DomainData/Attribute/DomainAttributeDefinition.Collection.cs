using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Generic Base class for Domain Attribute Definitions
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainAttributeDefinitionCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDomainAttributeKey>,
        IWriteData<IModelKey>, IWriteData<IDomainAttributeKey>,
        IRemoveItem<IDomainAttributeKey>
        where TItem : BindingTableRow, IDomainAttributeDefinitionItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainAttributeKey attributeKey)
        { return LoadCommand(connection, (null, attributeKey.AttributeId)); }

        /// <inheritdoc/>
        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? attributeId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeDefinition]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDomainAttributeKey attributeKey)
        { return SaveCommand(connection, (null, attributeKey.AttributeId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? attributeId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeDefinition]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);

            IEnumerable<TItem> data = this.Where(w => parameters.attributeId is null || w.AttributeId == parameters.attributeId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeDefinition]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDomainAttributeKey attributeItem)
        {
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
