using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Domain Attribute Properties
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class AttributePropertyCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IAttributeKey>,
        IWriteData<IModelKey>, IWriteData<IAttributeKey>,
        IRemoveItem<IAttributeKey>,
        ITemporalData<IModelKey>
        where TItem : AttributePropertyItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IAttributeKey attributeKey)
        { return LoadCommand(connection, attributeId: attributeKey.AttributeId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IAttributeKey attributeKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, attributeId: attributeKey.AttributeId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? attributeId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = AttributeProperty.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Attribute.AttributeId, attributeId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IAttributeKey attributeKey)
        { return SaveCommand(connection, attributeId: attributeKey.AttributeId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? attributeId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = AttributeProperty.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Attribute.AttributeId, attributeId);

            IEnumerable<TItem> data = this.Where(w =>
                (attributeId is null || w.AttributeId == attributeId));
            command.AddParameter(WriteData.Data, AttributeProperty.TableType, data);
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
