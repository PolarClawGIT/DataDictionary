using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Generic Base class for Scripting Document
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    [Obsolete]
    public class DocumentCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IDocumentKey>, IReadData<ITemplateKey>,
        IWriteData<IModelKey>, IWriteData<IDocumentKey>, IWriteData<ITemplateKey>,
        IRemoveItem<IDocumentKey>, IRemoveItem<ITemplateKey>,
        IReadTemporal<IModelKey>, IReadTemporal<IDocumentKey>
        where TItem : BindingTableRow, IDocumentItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null, documentId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDocumentKey documentKey)
        { return LoadCommand(connection, documentId: documentKey.DocumentId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDocumentKey documentKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, documentId: documentKey.DocumentId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

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
        public Command HistoryCommand(IConnection connection, IDocumentKey key)
        { return LoadCommand(connection, documentId: key.DocumentId, includeHistory: true); }

        private Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? documentId = null, Guid? templateId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Document.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Document.Identifier, documentId);
            command.AddParameter(Template.Identifier, templateId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDocumentKey documentKey)
        { return SaveCommand(connection, documentId: documentKey.DocumentId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ITemplateKey templateKey)
        { return SaveCommand(connection, documentId: templateKey.TemplateId); }

        private Command SaveCommand(IConnection connection,
            Guid? modelId = null, Guid? documentId = null, Guid? templateId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Document.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Document.Identifier, documentId);
            command.AddParameter(Template.Identifier, templateId);

            IEnumerable<TItem> data = this.Where(w =>
                (documentId is null || w.DocumentId == documentId) &&
                (templateId is null || w.TemplateId == templateId));
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

        /// <inheritdoc/>
        public virtual void Remove(ITemplateKey templateKey)
        {
            TemplateKey key = new TemplateKey(templateKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
