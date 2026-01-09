using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Document
    /// A Document represent the XML Input/Output that is produced by a Document.
    /// It also may be a independent document attached to the Model.
    /// This would allow for documents that are not generated from a Document.
    /// </summary>
    public interface IDocumentItem: IDocumentKey, IDocumentKeyName, ITemplateKey,
        ITemporalItem
    {
        /// <summary>
        /// XSLT Transform Script.
        /// </summary>
        /// <remarks>
        /// Root Node should be- xsl:stylesheet
        /// XML NameSpace- xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        /// </remarks>
        String? TransformScript { get; }

        /// <summary>
        /// Name of the Special Folder used as the Root Directory.
        /// </summary>
        /// <remarks>
        /// This uses an Enum that repensts locations in: Environment.SpecialFolder.UserProfile
        /// </remarks>
        String? SpecialFolder { get; }

        /// <summary>
        /// Input Directory off of the Root Directory where the XML Input file is located.
        /// </summary>
        String? InputDirectory { get; }

        /// <summary>
        /// Input File name for the XML Input file.
        /// </summary>
        String? InputFile { get; }

        /// <summary>
        /// Output Directory off of the Root Directory where the Output file is to be written.
        /// </summary>
        String? OutputDirectory { get; }

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
        public String? TransformScript
        {
            get { return GetValue(nameof(TransformScript)); }
            set { SetValue(nameof(TransformScript), value); }
        }

        /// <inheritdoc/>
        public String? SpecialFolder
        {
            get { return GetValue(nameof(SpecialFolder)); }
            set { SetValue(nameof(SpecialFolder), value); }
        }

        /// <inheritdoc/>
        public String? InputDirectory
        {
            get { return GetValue(nameof(InputDirectory)); }
            set { SetValue(nameof(InputDirectory), value); }
        }

        /// <inheritdoc/>
        public String? InputFile
        {
            get { return GetValue(nameof(InputFile)); }
            set { SetValue(nameof(InputFile), value); }
        }

        /// <inheritdoc/>
        public String? OutputDirectory
        {
            get { return GetValue(nameof(OutputDirectory)); }
            set { SetValue(nameof(OutputDirectory), value); }
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
            new DataColumn(nameof(TransformScript), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(SpecialFolder), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(InputDirectory), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(InputFile), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(OutputDirectory), typeof(String)){ AllowDBNull = true},
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
