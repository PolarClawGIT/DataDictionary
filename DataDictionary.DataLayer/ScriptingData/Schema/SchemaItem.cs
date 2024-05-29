using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema data.
    /// </summary>
    public interface ISchemaItem : ISchemaKey, ISchemaKeyName, IScopeKey
    {
        /// <summary>
        /// Description of the Schema.
        /// </summary>
        String? SchemaDescription { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema data.
    /// </summary>
    [Serializable]
    public class SchemaItem: BindingTableRow, ISchemaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            protected set { SetValue(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public string? SchemaTitle { get { return GetValue(nameof(SchemaTitle)); } set { SetValue(nameof(SchemaTitle), value); } }

        /// <inheritdoc/>
        public string? SchemaDescription { get { return GetValue(nameof(SchemaDescription)); } set { SetValue(nameof(SchemaDescription), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingSchema; } }

        /// <summary>
        /// Constructor for Scripting Transform
        /// </summary>
        public SchemaItem() : base()
        {
            if (SchemaId is null) { SchemaId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(SchemaTitle)) { SchemaTitle = "(new Schema)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaDescription), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SchemaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return SchemaTitle ?? String.Empty; }
    }
}
