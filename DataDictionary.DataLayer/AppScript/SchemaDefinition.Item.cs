using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting SchemaDefinition
    /// </summary>
    public interface ISchemaDefinitionItem : ISchemaDefinitionKey, ITemplateKey
    {
        /// <summary>
        /// Title of the Scripting Schema (aka Name of the Schema)
        /// </summary>
        String? SchemaTitle { get; }

        /// <summary>
        ///  ForEach Object of this type in the Model.
        ///  This impact file breaks as a new file is produced for each item of this scope.
        /// </summary>
        ScopeType ForEachScope { get; }

        /// <summary>
        /// Name of the Root Node. This will default to the name of the Object if not specified.
        /// </summary>
        String? RootNodeName { get; }

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
    /// Implementation for the Scripting SchemaDefinition.
    /// </summary>
    [Serializable]
    public class SchemaDefinitionItem : BindingTableRow, ISchemaDefinitionItem, ISerializable
    {
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
            protected set { SetValue(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public String? SchemaTitle
        {
            get { return GetValue(nameof(SchemaTitle)); }
            set { SetValue(nameof(SchemaTitle), value); }
        }

        /// <inheritdoc/>
        public ScopeType ForEachScope
        {
            get
            {
                String? value = GetValue(nameof(ForEachScope));
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set
            { SetValue(nameof(RootFolder), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? RootNodeName
        {
            get { return GetValue(nameof(RootNodeName)); }
            set { SetValue(nameof(RootNodeName), value); }
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
        protected SchemaDefinitionItem() : base()
        {
            if (SchemaId is null) { SchemaId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(SchemaTitle)) { SchemaTitle = "(new Schema)"; }
            if (String.IsNullOrWhiteSpace(FileExtension)) { FileExtension = "XML"; }

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
        public SchemaDefinitionItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ForEachScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RootNodeName), typeof(String)){ AllowDBNull = true},
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
        protected SchemaDefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return SchemaTitle ?? String.Empty; }
    }
}
