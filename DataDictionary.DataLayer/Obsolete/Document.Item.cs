using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Scripting Document
    /// A Document represent the XML Input/Output that is produced by a Document.
    /// It also may be a independent document attached to the Model.
    /// This would allow for documents that are not generated from a Document.
    /// </summary>
    [Obsolete]
    public interface IDocumentItem: IDocumentKey, IDocumentKeyName, ITemplateKey,
        ITemporalItem
    {
        /// <summary>
        /// Name of the Special Folder used as the Root Directory.
        /// </summary>
        /// <remarks>
        /// This uses an Enum that represents locations in: Environment.SpecialFolder.UserProfile
        /// </remarks>
        DirectoryType RootFolder { get; }

        /// <summary>
        /// Input Directory off of the Root Directory where the XML Input file is located.
        /// </summary>
        String? InputPath { get; }

        /// <summary>
        /// Input File name for the XML Input file.
        /// </summary>
        String? InputFile { get; }

        /// <summary>
        /// Process Directory off of the Root Directory where the XSL Process/Transform file is located.
        /// </summary>
        String? ProcessPath { get; }

        /// <summary>
        /// Process File name for the XSL Process/Transform file.
        /// </summary>
        String? ProcessFile { get; }

        /// <summary>
        /// Output Directory off of the Root Directory where the Output file is to be written.
        /// </summary>
        String? OutputPath { get; }

        /// <summary>
        /// Output File name for the result file.
        /// </summary>
        String? OutputFile { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Document data.
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
        public String? DocumentTitle
        {
            get { return GetValue(nameof(DocumentTitle)); }
            set { SetValue(nameof(DocumentTitle), value); }
        }

        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public DirectoryType RootFolder
        {
            get
            {
                String? value = GetValue(nameof(RootFolder));
                if (value.TryParse(out DirectoryType result))
                { return result; }
                else { return DirectoryType.Null; }
            }
            set
            { SetValue(nameof(RootFolder), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? InputPath
        {
            get { return GetValue(nameof(InputPath)); }
            set { SetValue(nameof(InputPath), value); }
        }

        /// <inheritdoc/>
        public String? InputFile
        {
            get { return GetValue(nameof(InputFile)); }
            set { SetValue(nameof(InputFile), value); }
        }

        /// <inheritdoc/>
        public String? ProcessPath
        {
            get { return GetValue(nameof(ProcessPath)); }
            set { SetValue(nameof(ProcessPath), value); }
        }

        /// <inheritdoc/>
        public String? ProcessFile
        {
            get { return GetValue(nameof(ProcessFile)); }
            set { SetValue(nameof(ProcessFile), value); }
        }

        /// <inheritdoc/>
        public String? OutputPath
        {
            get { return GetValue(nameof(OutputPath)); }
            set { SetValue(nameof(OutputPath), value); }
        }

        /// <inheritdoc/>
        public String? OutputFile
        {
            get { return GetValue(nameof(OutputFile)); }
            set { SetValue(nameof(OutputFile), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Data Source
        /// </summary>
        public DocumentItem() : base()
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

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DocumentId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(DocumentTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(RootFolder), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(InputPath), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(InputFile), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ProcessPath), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ProcessFile), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(OutputPath), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(OutputFile), typeof(String)){ AllowDBNull = true},
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
