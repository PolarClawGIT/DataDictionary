using DataDictionary.DataLayer.ApplicationData.Model;
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
    /// Generic Base class for Domain Attribute Properties
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainAttributePropertyCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>,
        IWriteData<IModelKey>,
        IRemoveData<IDomainAttributeKey>
        where TItem : BindingTableRow, IDomainAttributePropertyItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null)); }

        /// <inheritdoc/>
        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? attributeId, string? propertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeProperty]", this);
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
    /// Default List/Collection of Domain Attribute Properties
    /// </summary>
    public class DomainAttributePropertyCollection: DomainAttributePropertyCollection<DomainAttributePropertyItem>
    { }
}
