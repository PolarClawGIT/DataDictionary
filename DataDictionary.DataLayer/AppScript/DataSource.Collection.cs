// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Generic Base class for Scripting Data Source
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class DataSourceCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IDataSourceKey>, IReadData<ITemplateKey>,
        IWriteData<IModelKey>, IWriteData<IDataSourceKey>,
        IRemoveItem<IDataSourceKey>
        where TItem : BindingTableRow, IDataSourceItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null, templateId: null); }

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
        public Command LoadCommand(IConnection connection, IDataSourceKey dataSourceKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, dataSourceId: dataSourceKey.DataSourceId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? dataSourceId = null, Guid? templateId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DataSource.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(DataSource.DataSourceId, dataSourceId);
            command.AddParameter(Template.TemplateId, templateId);
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
            command.CommandText = DataSource.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(DataSource.DataSourceId, dataSourceId);

            IEnumerable<TItem> data = this.Where(w =>
                (dataSourceId is null || w.DataSourceId == dataSourceId));
            command.AddParameter(WriteData.Data, DataSource.TableType, data);
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
