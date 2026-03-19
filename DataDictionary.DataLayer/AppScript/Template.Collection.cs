using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Generic Base class for Scripting Template
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class TemplateCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<ITemplateKey>,
        IWriteData<IModelKey>, IWriteData<ITemplateKey>,
        IRemoveItem<ITemplateKey>,
        IReadTemporal<IModelKey>, IReadTemporal<ITemplateKey>
        where TItem : BindingTableRow, ITemplateItem, new()
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
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ITemplateKey key)
        { return LoadCommand(connection, templateId: key.TemplateId, includeHistory: true); }

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? templateId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Template.GetProcedure;
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
            command.CommandText = Template.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, templateId);

            IEnumerable<TItem> data = this.Where(w =>
                (templateId is null || w.TemplateId == templateId));
            command.AddParameter(WriteData.Data, Template.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ITemplateKey templateKey)
        {
            TemplateKey key = new TemplateKey(templateKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
