// Ignore Spelling: Utc

using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Domain Properties
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class PropertyCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IPropertyKey>,
        IWriteData<IModelKey>, IWriteData<IPropertyKey>,
        IDeleteData<IPropertyKey>,
        IRemoveItem<IPropertyKey>,
        IReadTemporal<IModelKey>
        where TItem : PropertyItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPropertyKey propertyKey)
        { return LoadCommand(connection, propertyId: propertyKey.PropertyId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPropertyKey propertyKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, propertyId: propertyKey.PropertyId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? propertyId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Property.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Property.Identifier, propertyId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId : modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IPropertyKey PropertyKey)
        { return SaveCommand(connection, propertyId: PropertyKey.PropertyId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? propertyId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Property.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Property.Identifier, propertyId);

            IEnumerable<TItem> data = this.Where(w =>
                (propertyId is null || w.PropertyId == propertyId));
            command.AddParameter(WriteData.Data, Property.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, IPropertyKey parameters)
        { return DeleteCommand(connection, (parameters.PropertyId, null)); }

        [Obsolete("Do not think this is needed")]
        Command DeleteCommand(IConnection connection, (Guid? PropertyId, String? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainProperty]";
            command.AddParameter("@PropertyId", parameters.PropertyId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IPropertyKey PropertyItem)
        {
            PropertyKey key = new PropertyKey(PropertyItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
