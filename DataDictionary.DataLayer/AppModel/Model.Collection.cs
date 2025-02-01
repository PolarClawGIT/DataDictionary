// Ignore Spelling: Utc

using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Models
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>,
        IWriteData, IWriteData<IModelKey>,
        IDeleteData<IModelKey>, IValidateList<ModelItem>,
        IRemoveItem<IModelKey>,
        ITemporalData<IModelKey>
        where TItem : ModelItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Model.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, modelId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Model.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);

            IEnumerable<TItem> data = this.Where(w =>
                (modelId is null || w.ModelId == modelId));
            command.AddParameter(WriteData.Data, Model.TableType, data);

            return command;
        }

        /// <summary>
        /// Removes the Model from the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, IModelKey parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procDeleteModel]";
            command.AddParameter("@ModelId", parameters.ModelId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IModelKey modelItem)
        {
            ModelKey key = new ModelKey(modelItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        [Obsolete("Not in Use")]
        public IReadOnlyList<ModelItem> Validate()
        {
            List<ModelItem> result = new List<ModelItem>();

            foreach (ModelItem item in this)
            {
                item.ClearRowErrors();
                if (!item.Validate())
                { result.Add(item); }
            }

            return result;
        }

    }
}
