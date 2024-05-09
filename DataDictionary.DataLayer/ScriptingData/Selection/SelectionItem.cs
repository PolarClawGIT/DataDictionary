using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;
using DataDictionary.DataLayer.ScriptingData.Transform;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Scripting Selection data.
    /// </summary>
    public interface ISelectionItem : ISelectionKey, ITransformKey, ISchemaKey, IScopeKey
    {
        /// <summary>
        /// Title of the Selection.
        /// </summary>
        String? SelectionTitle { get; }

        /// <summary>
        /// Description of the Selection.
        /// </summary>
        String? SelectionDescription { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Selection data.
    /// </summary>
    [Serializable]
    public class SelectionItem : BindingTableRow, ISelectionItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SelectionId
        {
            get { return GetValue<Guid>(nameof(SelectionId)); }
            protected set { SetValue(nameof(SelectionId), value); }
        }

        /// <inheritdoc/>
        public String? SelectionTitle { get { return GetValue(nameof(SelectionTitle)); } set { SetValue(nameof(SelectionTitle), value); } }

        /// <inheritdoc/>
        public String? SelectionDescription { get { return GetValue(nameof(SelectionDescription)); } set { SetValue(nameof(SelectionDescription), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingSelection; } }

        /// <inheritdoc/>
        public Guid? TransformId
        {
            get { return GetValue<Guid>(nameof(TransformId)); }
            set { SetValue(nameof(TransformId), value); }
        }

        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            set { SetValue(nameof(SchemaId), value); }
        }

        /// <summary>
        /// Constructor for Scripting Transform
        /// </summary>
        public SelectionItem() : base()
        {
            if (SelectionId is null) { SelectionId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(SelectionTitle)) { SelectionTitle = "(new Selection)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(SelectionId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SelectionTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SelectionDescription), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TransformId), typeof(Guid)){ AllowDBNull = true},
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
        protected SelectionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return SelectionTitle ?? String.Empty; }
    }
}
