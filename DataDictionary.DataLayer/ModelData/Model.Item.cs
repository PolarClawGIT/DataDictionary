using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ModelData
{
    /// <summary>
    /// Interface for the Model.
    /// </summary>
    public interface IModelItem : IModelKey, IScopeType
    {
        /// <summary>
        /// Title for the Model.
        /// </summary>
        string? ModelTitle { get; set; }

        /// <summary>
        /// Description for the Model.
        /// </summary>
        string? ModelDescription { get; set; }
    }

    /// <summary>
    /// Implementation of the Model data.
    /// </summary>
    [Serializable]
    public class ModelItem : BindingTableRow, IModelItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? ModelId { get { return GetValue<Guid>(nameof(ModelId)); } protected set { SetValue(nameof(ModelId), value); } }

        /// <inheritdoc/>
        public string? ModelTitle { get { return GetValue(nameof(ModelTitle)); } set { SetValue(nameof(ModelTitle), value); } }

        /// <inheritdoc/>
        public string? ModelDescription { get { return GetValue(nameof(ModelDescription)); } set { SetValue(nameof(ModelDescription), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.Model;

        /// <summary>
        /// Constructor for the Model data
        /// </summary>
        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(ModelId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ModelTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ModelDescription), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public bool Validate()
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(ModelTitle))
            { SetRowError("[ModelTitle] cannot be empty"); }
            else if (ModelId == Guid.Empty)
            { SetRowError("[ModelId] cannot be empty"); }
            else { result = true; }

            return result;
        }

        #region ISerializable
        /// <summary>
        /// Serialization constructor for Model.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ModelItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
