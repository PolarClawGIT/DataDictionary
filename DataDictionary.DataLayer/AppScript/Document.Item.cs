using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Document
    /// </summary>
    public interface IDocumentItem : IDocumentKey, ITemplateKey, ISchemaDefinitionKey, ITransformKey, ITemplateObjectKey
    {
        /// <summary>
        /// Filename of the Object that this document represents.
        /// </summary>
        String? FileName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Document.
    /// </summary>
    [Serializable]
    public class DocumentItem : BindingTableRow, IDocumentItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? DocumentId
        {
            get { return GetValue<Guid>(nameof(DocumentId)); }
            protected set { SetValue(nameof(DocumentId), value); }
        }

        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            set { SetValue(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)); }
            protected set { SetValue(nameof(TransformId), value); }
        }

        /// <inheritdoc/>
        public Guid? ObjectId
        {
            get { return GetValue<Guid>(nameof(ObjectId)); }
            protected set { SetValue(nameof(ObjectId), value); }
        }


        /// <inheritdoc/>
        public String? FileName
        {
            get { return GetValue(nameof(FileName)); }
            set { SetValue(nameof(FileName), value); }
        }


        /// <inheritdoc/>
        public ITemporal Temporal { get; }


        /// <summary>
        /// Constructor for Scripting Document
        /// </summary>
        public DocumentItem() : base()
        {
            if (DocumentId is null) { DocumentId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(FileName)) { FileName = "newDocument"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Document
        /// </summary>
        public DocumentItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DocumentId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TransformId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(FileName), typeof(String)){ AllowDBNull = true},
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
        { return FileName ?? String.Empty; }
    }
}
