using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Transform
    /// </summary>
    public interface ITransformItem : ITransformKey, ITemplateKey, ISchemaDefinitionKey
    {
        /// <summary>
        /// Title of the Scripting Transform (aka Name of the Transform)
        /// </summary>
        String? TransformTitle { get; }

        /// <summary>
        /// XSLT Transform Script.
        /// </summary>
        /// <remarks>
        /// Root Node should be- xsl:stylesheet
        /// XML NameSpace- xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        /// </remarks>
        String? TransformScript { get; }

        /// <summary>
        /// The name of the file when TransformScript is stored as a File.
        /// </summary>
        String? TransformFileName { get; }

        /// <summary>
        /// Name of the Special Folder used as the Root Directory.
        /// </summary>
        /// <remarks>
        /// This uses an Enum that represents locations in: Environment.SpecialFolder.UserProfile
        /// </remarks>
        DirectoryType RootFolder { get; }

        /// <summary>
        /// Relative Directory off of the Root Directory where the XML Input file is located.
        /// </summary>
        String? RelativePath { get; }

        /// <summary>
        /// Prefix to add to the front of the file name.
        /// </summary>
        String? FilePrefix { get; }

        /// <summary>
        /// Prefix to add to the end of the file name.
        /// </summary>
        String? FileSuffix { get; }

        /// <summary>
        /// File Extension to add to the end of the file name.
        /// </summary>
        String? FileExtension { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Transform.
    /// </summary>
    [Serializable]
    public class TransformItem : BindingTableRow, ITransformItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)); }
            protected set { SetValue(nameof(TransformId), value); }
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
        public String? TransformTitle
        {
            get { return GetValue(nameof(TransformTitle)); }
            set { SetValue(nameof(TransformTitle), value); }
        }

        /// <inheritdoc/>
        public String? TransformScript
        {
            get { return GetValue(nameof(TransformScript)); }
            set { SetValue(nameof(TransformScript), value); }
        }

        /// <inheritdoc/>
        public String? TransformFileName
        {
            get { return GetValue(nameof(TransformFileName)); }
            set { SetValue(nameof(TransformFileName), value); }
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
        public String? RelativePath
        {
            get { return GetValue(nameof(RelativePath)); }
            set { SetValue(nameof(RelativePath), value); }
        }

        /// <inheritdoc/>
        public String? FilePrefix
        {
            get { return GetValue(nameof(FilePrefix)); }
            set { SetValue(nameof(FilePrefix), value); }
        }

        /// <inheritdoc/>
        public String? FileSuffix
        {
            get { return GetValue(nameof(FileSuffix)); }
            set { SetValue(nameof(FileSuffix), value); }
        }

        /// <inheritdoc/>
        public String? FileExtension
        {
            get { return GetValue(nameof(FileExtension)); }
            set { SetValue(nameof(FileExtension), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Schema Definition
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected TransformItem() : base()
        {
            if (TransformId is null) { TransformId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(TransformTitle)) { TransformTitle = "(new Transform)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Schema Definition
        /// </summary>
        public TransformItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TransformId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TransformTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TransformScript), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(TransformFileName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RootFolder), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RelativePath), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(FilePrefix), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(FileSuffix), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(FileExtension), typeof(String)){ AllowDBNull = true},
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
        protected TransformItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return TransformTitle ?? String.Empty; }
    }
}
