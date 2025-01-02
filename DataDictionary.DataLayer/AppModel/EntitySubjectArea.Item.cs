using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Entity Subject Area Item
    /// </summary>
    public interface IEntitySubjectAreaItem : IEntityKey, ISubjectAreaKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Entity Subject Area Item
    /// </summary>
    public class EntitySubjectAreaItem : BindingTableRow, IEntitySubjectAreaItem, ISerializable
    {

        /// <inheritdoc/>
        public Guid? EntityId
        {
            get { return GetValue<Guid>(nameof(EntityId)); }
            protected set { SetValue(nameof(EntityId), value); }
        }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        {
            get { return GetValue<Guid>(nameof(SubjectAreaId)); }
            protected set { SetValue(nameof(SubjectAreaId), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = false},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Entity Subject Area Items
        /// </summary>
        public EntitySubjectAreaItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Entity Subject Area Items
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="subject"></param>
        public EntitySubjectAreaItem(IEntityKey Entity, ISubjectAreaKey subject) : this()
        {
            EntityId = Entity.EntityId;
            SubjectAreaId = subject.SubjectAreaId;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected EntitySubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
