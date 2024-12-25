using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Entity Item
    /// </summary>
    public interface IEntityItem : IEntity, IEntityKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Entity Item
    /// </summary>
    [Serializable]
    public class EntityItem : BindingTableRow, IEntityItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId
        {
            get { return GetValue<Guid>(nameof(EntityId)); }
            protected set { SetValue(nameof(EntityId), value); }
        }

        /// <inheritdoc/>
        public String? EntityTitle
        {
            get { return GetValue(nameof(EntityTitle)); }
            set { SetValue(nameof(EntityTitle), value); }
        }

        /// <inheritdoc/>
        public String? EntityDescription
        {
            get { return GetValue(nameof(EntityDescription)); }
            set { SetValue(nameof(EntityDescription), value); }
        }

        /// <inheritdoc/>
        public string? EntityName
        {
            get { return GetValue(nameof(EntityName)); }
            set { SetValue(nameof(EntityName), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Domain Entity Item
        /// </summary>
        public EntityItem() : base()
        {
            if (EntityId is null) { EntityId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(EntityTitle)) { EntityTitle = "(new Entity)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(EntityTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(EntityDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(EntityName), typeof(string)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected EntityItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        public override String ToString()
        { if (EntityTitle is not null) { return EntityTitle; } else { return string.Empty; } }
    }
}
