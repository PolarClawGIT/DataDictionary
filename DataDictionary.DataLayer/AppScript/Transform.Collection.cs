using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppScript
{

    /// <summary>
    /// Generic Base class for Scripting Transform
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class TransformCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<ITemplateKey>,
        IWriteData<IModelKey>, IWriteData<ITemplateKey>,
        IRemoveItem<ITemplateKey>,
        IReadTemporal<IModelKey>, IReadTemporal<ITemplateKey>
        where TItem : BindingTableRow, ITransformItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null, TemplateId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITemplateKey TemplateKey)
        { return LoadCommand(connection, TemplateId: TemplateKey.TemplateId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITemplateKey TemplateKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, TemplateId: TemplateKey.TemplateId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ITemplateKey key)
        { return LoadCommand(connection, TemplateId: key.TemplateId, includeHistory: true); }

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? TemplateId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Transform.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, TemplateId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ITemplateKey TemplateKey)
        { return SaveCommand(connection, TemplateId: TemplateKey.TemplateId); }

        private Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? TemplateId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Transform.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, TemplateId);

            IEnumerable<TItem> data = this.Where(w =>
                (TemplateId is null || w.TemplateId == TemplateId));
            command.AddParameter(WriteData.Data, Transform.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ITemplateKey TemplateKey)
        {
            TemplateKey key = new TemplateKey(TemplateKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
