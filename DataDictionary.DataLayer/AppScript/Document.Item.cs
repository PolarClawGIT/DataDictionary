using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Document
    /// </summary>
    public interface IDocumentItem : IDocumentKey, ITemplateKey, ITransformKey, ISchemaDefinitionKey
    {
        /// <summary>
        /// Description of the Template Document
        /// </summary>
        String? DocumentTitle { get; }

        /// <summary>
        /// The name of the Structured data File (XML, JSON, ...).
        /// </summary>
        String StructuredFileName { get; }

        /// <summary>
        /// The name of the Transform File ().
        /// </summary>
        String TransformFileName { get; }

        /// <summary>
        /// Name of the Scripted File. This is the Output of the Transform process.
        /// </summary>
        String ScriptedFileName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Document.
    /// </summary>
    [Serializable]
    public class DocumentItem : BindingTableRow, IDocumentItem, ISerializable
    {
        /// <inheritdoc/>
        public virtual Guid? DocumentId
        {
            get { return GetValue<Guid>(nameof(DocumentId)); }
            protected set { SetValue(nameof(DocumentId), value); }
        }

        /// <inheritdoc/>
        public virtual String? DocumentTitle
        {
            get { return GetValue(nameof(DocumentTitle)); }
            set { SetValue(nameof(DocumentTitle), value); }
        }

        /// <inheritdoc/>
        public virtual Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)) ?? Guid.Empty; }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public virtual Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)) ?? Guid.Empty; }
            set
            {
                if (value == Guid.Empty) { SetValue(nameof(SchemaId), null); }
                else { SetValue<Guid>(nameof(SchemaId), value); }
            }
        }

        /// <inheritdoc/>
        public virtual Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)) ?? Guid.Empty; }
            set
            {
                if (value == Guid.Empty) { SetValue(nameof(TransformId), null); }
                else { SetValue(nameof(TransformId), value); }
            }
        }

        /// <inheritdoc/>
        public virtual String TransformFileName
        {
            get { return GetValue(nameof(TransformFileName)) ?? String.Empty; }
            set { SetValue(nameof(TransformFileName), value); }
        }

        /// <inheritdoc/>
        public virtual String StructuredFileName
        {
            get { return GetValue(nameof(StructuredFileName)) ?? String.Empty; }
            set { SetValue(nameof(StructuredFileName), value); }
        }

        /// <inheritdoc/>
        public virtual String ScriptedFileName
        {
            get { return GetValue(nameof(ScriptedFileName)) ?? String.Empty; }
            set { SetValue(nameof(ScriptedFileName), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Schema Document
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected DocumentItem() : base()
        {
            if (DocumentId is null) { DocumentId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(DocumentTitle)) { DocumentTitle = "(new Document)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Schema Document
        /// </summary>
        public DocumentItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DocumentId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(DocumentTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(StructuredFileName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(TransformId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TransformFileName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ScriptedFileName), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DocumentItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return DocumentTitle ?? String.Empty; }

    }
}
