using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Item
    /// </summary>
    public interface IDomainEntityItem : IDomainEntityKey, IModelSubjectAreaKey, IDataItem
    {
        /// <summary>
        /// Title of the Domain Entity (aka Name of the Entity)
        /// </summary>
        String? EntityTitle { get; }

        /// <summary>
        /// Description of the Domain Entity
        /// </summary>
        String? EntityDescription { get; set; }
    }

    /// <summary>
    /// Implementation for Domain Entity Item
    /// </summary>
    [Serializable]
    public class DomainEntityItem : BindingTableRow, IDomainEntityItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>("EntityId"); } protected set { SetValue("EntityId", value); } }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        { get { return GetValue<Guid>("SubjectAreaId"); } set { SetValue("SubjectAreaId", value); } }

        /// <inheritdoc/>
        public string? EntityTitle { get { return GetValue("EntityTitle"); } set { SetValue("EntityTitle", value); } }

        /// <inheritdoc/>
        public string? EntityDescription { get { return GetValue("EntityDescription"); } set { SetValue("EntityDescription", value); } }

        /// <summary>
        /// Constructor for Domain Entity Item
        /// </summary>
        public DomainEntityItem() : base()
        {
            if (EntityId is null) { EntityId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(EntityTitle)){ EntityTitle = "(new Entity)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("SubjectAreaId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("EntityTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("EntityDescription", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntityItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (EntityTitle is not null) { return EntityTitle; } else { return string.Empty; } }
    }
}
