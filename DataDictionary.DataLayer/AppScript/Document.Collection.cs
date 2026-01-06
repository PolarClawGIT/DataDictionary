using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Generic Base class for Scripting Document
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class DocumentCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IDocumentKey>,
        IWriteData<IModelKey>, IWriteData<IDocumentKey>,
        IRemoveItem<IDocumentKey>,
        IReadTemporal<IModelKey>, IReadTemporal<IDocumentKey>
        where TItem : BindingTableRow, IDocumentItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null, DocumentId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDocumentKey documentKey)
        { return LoadCommand(connection, DocumentId: documentKey.DocumentId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDocumentKey documentKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, DocumentId: documentKey.DocumentId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IDocumentKey key)
        { return LoadCommand(connection, DocumentId: key.DocumentId, includeHistory: true); }

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? DocumentId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Document.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Document.DocumentId, DocumentId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDocumentKey documentKey)
        { return SaveCommand(connection, DocumentId: documentKey.DocumentId); }

        private Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? DocumentId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Document.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Document.DocumentId, DocumentId);

            IEnumerable<TItem> data = this.Where(w =>
                (DocumentId is null || w.DocumentId == DocumentId));
            command.AddParameter(WriteData.Data, Document.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDocumentKey documentKey)
        {
            DocumentKey key = new DocumentKey(documentKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
