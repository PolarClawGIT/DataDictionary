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
    /// Generic Base class for Domain Attribute Aliases
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainAttributeAliasCollection<TItem> : BindingTable<TItem>, IReadData<IModelKey>, IWriteData<IModelKey>
        where TItem : DomainAttributeAliasItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null, null, null)); }

        /// <inheritdoc/>
        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? attributeId, string? catalogName, string? schemaName, string? objectName, string? elementName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeAlias]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ObjectName", parameters.objectName);
            command.AddParameter("@ElementName", parameters.elementName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeAlias]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeAlias]", this);
            return command;
        }
    }

    /// <summary>
    /// Default List/Collection of Domain Attribute Aliases
    /// </summary>
    public class DomainAttributeAliasCollection : DomainAttributeAliasCollection<DomainAttributeAliasItem>
    { }
}
