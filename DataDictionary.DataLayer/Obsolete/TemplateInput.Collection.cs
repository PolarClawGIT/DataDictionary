using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Generic Base class for Scripting Template Input Data Source
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    [Obsolete]
    public class TemplateInputCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ITemplateKey>,
        IWriteData<IModelKey>, IWriteData<ITemplateKey>,
        IRemoveItem<ITemplateKey>, IRemoveItem<IDataSourceKey>
        where TItem : BindingTableRow, ITemplateInputItem, new()
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

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? templateId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = TemplateInput.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, templateId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ITemplateKey templateKey)
        { return SaveCommand(connection, templateId: templateKey.TemplateId); }

        private Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? templateId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = TemplateInput.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, templateId);

            IEnumerable<TItem> data = this.Where(w =>
                (templateId is null || w.TemplateId == templateId));
            command.AddParameter(WriteData.Data, TemplateInput.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ITemplateKey templateKey)
        {
            TemplateKey key = new TemplateKey(templateKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IDataSourceKey dataSourceKey)
        {
            DataSourceKey key = new DataSourceKey(dataSourceKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
