using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Model Subject Area
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelSubjectAreaCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ISubjectAreaKey>,
        IWriteData<IModelKey>, IWriteData<ISubjectAreaKey>,
        IDeleteData<IModelKey>, IDeleteData<ISubjectAreaKey>,
        IRemoveItem<ISubjectAreaKey>,
        ITemporalData<IModelKey>, ITemporalData<ISubjectAreaKey>
        where TItem : SubjectAreaItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISubjectAreaKey subjectAreaKey)
        { return LoadCommand(connection, subjectAreaId: subjectAreaKey.SubjectAreaId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ISubjectAreaKey subjectAreaKey)
        { return LoadCommand(connection, subjectAreaId: subjectAreaKey.SubjectAreaId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? subjectAreaId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SubjectArea.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(SubjectArea.SubjectAreaId, subjectAreaId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISubjectAreaKey subjectAreaKey)
        { return SaveCommand(connection, subjectAreaId: subjectAreaKey.SubjectAreaId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? subjectAreaId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SubjectArea.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(SubjectArea.SubjectAreaId, subjectAreaId);

            IEnumerable<TItem> data = this.Where(w =>
                (subjectAreaId is null || w.SubjectAreaId == subjectAreaId));
            command.AddParameter(WriteData.Data, SubjectArea.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, IModelKey key)
        { return DeleteCommand(connection, (key.ModelId, null)); }

        /// <inheritdoc/>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, ISubjectAreaKey key)
        { return DeleteCommand(connection, (null, key.SubjectAreaId)); }

        [Obsolete("Do not think this is needed")]
        Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? subjectAreaId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SubjectAreaId", parameters.subjectAreaId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ISubjectAreaKey subjectAreaItem)
        {
            SubjectAreaKey key = new SubjectAreaKey(subjectAreaItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
