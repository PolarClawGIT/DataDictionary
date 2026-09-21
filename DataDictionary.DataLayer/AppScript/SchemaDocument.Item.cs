using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Schema Document
    /// </summary>
    public interface ISchemaDocumentItem : ITemplateKey, ISchemaDefinitionKey, IDocumentKey, ITemplateObjectItem
    {
        /// <summary>
        /// The name of the Schema File (XML).
        /// </summary>
        String SchemaFileName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Document.
    /// </summary>
    [Serializable]
    public class SchemaDocumentItem : BindingTableRow, ISchemaDocumentItem, ISerializable
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
            protected set { SetValue(nameof(SchemaId), value); }
        }


        /// <inheritdoc/>
        public String SchemaFileName
        {
            get { return GetValue(nameof(SchemaFileName)) ?? String.Empty; }
            set { SetValue(nameof(SchemaFileName), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <inheritdoc/>
        public virtual ScopeType ObjectScope
        {
            get
            {
                String? value = GetValue(nameof(ObjectScope));
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set
            { SetValue(nameof(ObjectScope), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public virtual String? ObjectPath
        {
            get { return GetValue(nameof(ObjectPath)); }
            set { SetValue(nameof(ObjectPath), value); }
        }

        /// <inheritdoc/>
        public Boolean IsExcluded
        {
            get
            {
                if (GetValue<bool>(nameof(IsExcluded), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set { SetValue<Boolean>(nameof(IsExcluded), value); }
        }

        /// <inheritdoc/>
        public Boolean KeepOrphaned
        {
            get
            {
                if (GetValue<bool>(nameof(KeepOrphaned), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set { SetValue<Boolean>(nameof(KeepOrphaned), value); }
        }

        /// <summary>
        /// Constructor for Scripting Schema Document
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected SchemaDocumentItem() : base()
        {
            if (DocumentId is null) { DocumentId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(SchemaFileName)) { SchemaFileName = "newDocument"; }

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
        public SchemaDocumentItem(ITemplateKey template, ISchemaDefinitionKey schema) : this()
        {
            TemplateId = template.TemplateId;
            SchemaId = schema.SchemaId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DocumentId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectPath), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsExcluded), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(KeepOrphaned), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaFileName), typeof(String)){ AllowDBNull = true},
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
        protected SchemaDocumentItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return SchemaFileName ?? String.Empty; }

    }
}
