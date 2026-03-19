// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Generic Base class for Scripting Data Object
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    [Obsolete]
    public abstract class DataObjectCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDataSourceKey>, IReadData<ITemplateKey>,
        IWriteData<IModelKey>, IWriteData<IDataSourceKey>,
        IRemoveItem<IDataSourceKey>,
        IReadTemporal<IModelKey>, IReadTemporal<IDataSourceKey>
        where TItem : BindingTableRow, IDataObjectItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITemplateKey templateKey)
        { return LoadCommand(connection, templateId: templateKey.TemplateId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITemplateKey templateKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, templateId: templateKey.TemplateId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDataSourceKey dataSourceKey)
        { return LoadCommand(connection, dataSourceId: dataSourceKey.DataSourceId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDataSourceKey key, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, dataSourceId: key.DataSourceId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IDataSourceKey key)
        { return LoadCommand(connection, dataSourceId: key.DataSourceId, includeHistory: true); }

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? dataSourceId = null, Guid? templateId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DataObject.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(DataSource.Identifier, dataSourceId);
            command.AddParameter(Template.Identifier, templateId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDataSourceKey dataSourceKey)
        { return SaveCommand(connection, dataSourceId: dataSourceKey.DataSourceId); }

        private Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? dataSourceId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DataObject.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(DataSource.Identifier, dataSourceId);

            IEnumerable<TItem> data = this.Where(w =>
                (dataSourceId is null || w.DataSourceId == dataSourceId));
            command.AddParameter(WriteData.Data, DataObject.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDataSourceKey templateKey)
        {
            DataSourceKey key = new DataSourceKey(templateKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
