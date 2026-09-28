using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentItem
    {
        /// <summary>
        /// The name of the Schema File (XML).
        /// </summary>
        String DataFileName { get; }
    }

    /// <summary>
    /// Interface for the Scripting Schema Document
    /// </summary>
    public interface ISchemaDocumentItem : ITemplateKey, ISchemaDefinitionKey, IDocumentKey, IDocumentItem, ITemplateObjectItem
    { }

    /// <summary>
    /// Implementation for the Scripting Schema Document.
    /// </summary>
    [Serializable]
    public class SchemaDocumentItem : BindingTableRow, ISchemaDocumentItem, ISerializable
    {
        /// <inheritdoc/>
        public virtual Guid? DocumentId
        {
            get { return GetValue<Guid>(nameof(DocumentId)); }
            protected set { SetValue(nameof(DocumentId), value); }
        }

        /// <inheritdoc/>
        public virtual Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public virtual Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            protected set { SetValue(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public virtual String DataFileName
        {
            get { return GetValue(nameof(DataFileName)) ?? String.Empty; }
            set { SetValue(nameof(DataFileName), value); }
        }

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
        public virtual Boolean IsExcluded
        {
            get
            {
                if (GetValue<bool>(nameof(IsExcluded), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set { SetValue<Boolean>(nameof(IsExcluded), value); }
        }

        /// <inheritdoc/>
        public virtual Boolean KeepOrphaned
        {
            get
            {
                if (GetValue<bool>(nameof(KeepOrphaned), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set { SetValue<Boolean>(nameof(KeepOrphaned), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Schema Document
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected SchemaDocumentItem() : base()
        {
            if (DocumentId is null) { DocumentId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(DataFileName)) { DataFileName = "newDocument"; }

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
            new DataColumn(nameof(DataFileName), typeof(String)){ AllowDBNull = true},
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
        { return DataFileName ?? String.Empty; }

    }
}
