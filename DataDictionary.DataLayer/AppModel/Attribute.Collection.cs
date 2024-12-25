using DataDictionary.DataLayer;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Domain Attributes
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainAttributeCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IAttributeKey>,
        IWriteData<IModelKey>, IWriteData<IAttributeKey>,
        IDeleteData<IModelKey>, IDeleteData<IAttributeKey>,
        IRemoveItem<IAttributeKey>,
        ITemporalData<IModelKey>, ITemporalData<IAttributeKey>
        where TItem : AttributeItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IAttributeKey attributeKey)
        { return LoadCommand(connection, attributeId: attributeKey.AttributeId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IAttributeKey attributeKey)
        { return LoadCommand(connection, attributeId: attributeKey.AttributeId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? attributeId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Attribute.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Attribute.AttributeId, attributeId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId : modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IAttributeKey attributeKey)
        { return SaveCommand(connection, attributeId: attributeKey.AttributeId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? attributeId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Attribute.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Attribute.AttributeId, attributeId);

            IEnumerable<TItem> data = this.Where(w =>
                (attributeId is null || w.AttributeId == attributeId));
            command.AddParameter(WriteData.Data, Attribute.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, IAttributeKey parameters)
        { return DeleteCommand(connection, (null, parameters.AttributeId)); }

        /// <inheritdoc/>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, IModelKey parameters)
        { return DeleteCommand(connection, (parameters.ModelId, null)); }

        [Obsolete("Do not think this is needed")]
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
        public virtual void Remove(IAttributeKey attributeItem)
        {
            AttributeKey key = new AttributeKey(attributeItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
