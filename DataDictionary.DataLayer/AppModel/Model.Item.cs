using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model.
    /// </summary>
    public interface IModelItem : IModel, IModelKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation of the Model data.
    /// </summary>
    [Serializable]
    public class ModelItem : BindingTableRow, IModelItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? ModelId { get { return GetValue<Guid>(nameof(ModelId)); } protected set { SetValue(nameof(ModelId), value); } }

        /// <inheritdoc/>
        public String? ModelTitle { get { return GetValue(nameof(ModelTitle)); } set { SetValue(nameof(ModelTitle), value); } }

        /// <inheritdoc/>
        public String? ModelDescription { get { return GetValue(nameof(ModelDescription)); } set { SetValue(nameof(ModelDescription), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for the Model data
        /// </summary>
        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ModelId), typeof(Guid)) { AllowDBNull = false},
            new DataColumn(nameof(ModelTitle), typeof(String)) { AllowDBNull = false},
            new DataColumn(nameof(ModelDescription), typeof(String)) { AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        [Obsolete("Not in Use")]
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
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion
    }
}
