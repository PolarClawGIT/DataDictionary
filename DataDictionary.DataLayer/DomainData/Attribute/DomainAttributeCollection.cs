using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Generic Base class for Domain Attributes
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainAttributeCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDomainAttributeKey>,
        IWriteData<IModelKey>, IWriteData<IDomainAttributeKey>,
        IDeleteData<IModelKey>, IDeleteData<IDomainAttributeKey>,
        IRemoveData<IDomainAttributeKey>
        where TItem : BindingTableRow, IDomainAttributeItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainAttributeKey attributeKey)
        { return LoadCommand(connection, (null, attributeKey.AttributeId, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? attributeId, string? attributeTitle, bool? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttribute]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            command.AddParameter("@AttributeTitle", parameters.attributeTitle);
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
            command.CommandText = "[App_DataDictionary].[procSetDomainAttribute]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttribute]", this);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IDomainAttributeKey parameters)
        { return DeleteCommand(connection, (null, parameters.AttributeId)); }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelKey parameters)
        { return DeleteCommand(connection, (parameters.ModelId, null)); }

        Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? attributeId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttribute]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);

            return command;
        }

        /// <inheritdoc/>
        public void Remove(IDomainAttributeKey attributeItem)
        {
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Domain Attributes
    /// </summary>
    public class DomainAttributeCollection: DomainAttributeCollection<DomainAttributeItem>
    { }
}
