using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Transform Document
    /// </summary>
    public interface ITransformDocumentItem : IDocumentItem, ITemplateKey, ITransformKey, IDocumentKey
    {
        /// <summary>
        /// DocumentID of the Schema Document that is the Source of the Transform.
        /// </summary>
        [Obsolete("Need to get rid of this. Use a file reference instead.")]
        Guid? SchemaDocumentId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Transform Document.
    /// </summary>
    [Serializable]
    public class TransformDocumentItem : BindingTableRow, ITransformDocumentItem, ISerializable
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
        public Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)); }
            set { SetValue(nameof(TransformId), value); }
        }


        /// <inheritdoc/>
        [Obsolete("Need to get rid of this. Use a file reference instead.")]
        public Guid? SchemaDocumentId
        {
            get { return GetValue<Guid>(nameof(SchemaDocumentId)); }
            set { SetValue(nameof(SchemaDocumentId), value); }
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
        /// Constructor for Scripting Transform Document
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected TransformDocumentItem() : base()
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
        /// Constructor for Scripting Transform Document
        /// </summary>
        public TransformDocumentItem(ITemplateKey template, ITransformKey transform) : this()
        {
            TemplateId = template.TemplateId;
            TransformId = transform.TransformId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DocumentId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TransformId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaDocumentId), typeof(Guid)){ AllowDBNull = true},
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
        protected TransformDocumentItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
