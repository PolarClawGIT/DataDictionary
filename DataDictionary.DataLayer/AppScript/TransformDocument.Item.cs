using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Transform Document
    /// </summary>
    public interface ITransformDocumentItem : ITemplateKey, ITransformKey, IDocumentKey, IDocumentNameKey
    {
        /// <summary>
        /// Name of the Scripted File. This is the Output of the Transform process.
        /// </summary>
        String ScriptedFileName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Transform Document.
    /// </summary>
    [Serializable]
    public class TransformDocumentItem : BindingTableRow, ITransformDocumentItem, ISerializable
    {
        /// <inheritdoc/>
        public virtual Guid? DocumentId
        {
            get { return GetValue<Guid>(nameof(DocumentId)); }
            protected set { SetValue(nameof(DocumentId), value); }
        }

        /// <inheritdoc/>
        public virtual Guid? TemplateId
        {   // Database Layer determines TemplateId from the TransformId.
            // Business Layer cannot make this determination.
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public virtual Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)); }
            set { SetValue(nameof(TransformId), value); }
        }

        /// <inheritdoc/>
        public virtual String DataFileName
        {
            get { return GetValue(nameof(DataFileName)) ?? String.Empty; }
            set { SetValue(nameof(DataFileName), value); }
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
        /// Constructor for Scripting Transform Document
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected TransformDocumentItem() : base()
        {
            if (DocumentId is null) { DocumentId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(ScriptedFileName)) { ScriptedFileName = "newDocument"; }

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
            new DataColumn(nameof(DataFileName), typeof(String)){ AllowDBNull = true},
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
        { return ScriptedFileName ?? String.Empty; }

    }
}
