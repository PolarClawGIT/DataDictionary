using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Item
    /// </summary>
    public interface IDomainEntityItem : IDomainEntityKey, IDomainEntityUniqueKey, IDataItem
    {
        /// <summary>
        /// Description of the Domain Entity
        /// </summary>
        String? EntityDescription { get; set; }

        /// <summary>
        /// The Entity that this is a Sub Type Of. Aka, the parent Entity.
        /// </summary>
        Guid? TypeOfEntityId { get; set; }

        /// <summary>
        /// The Title for Entity that this is a Sub Type Of. Aka, the parent Entity.
        /// </summary>
        String? TypeOfEntityTitle { get; }
    }

    /// <summary>
    /// Implementation for Domain Entity Item
    /// </summary>
    [Serializable]
    public class DomainEntityItem : BindingTableRow, IDomainEntityItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId
        {
            get { return GetValue<Guid>("EntityId"); }
            protected set { SetValue("EntityId", value); }
        }

        /// <inheritdoc/>
        public string? EntityTitle
        {
            get { return GetValue("EntityTitle"); }
            set { SetValue("EntityTitle", value); }
        }

        /// <inheritdoc/>
        public string? EntityDescription
        {
            get { return GetValue("EntityDescription"); }
            set { SetValue("EntityDescription", value); }
        }

        /// <inheritdoc/>
        public Guid? TypeOfEntityId
        {
            get { return GetValue<Guid>("TypeOfEntityId"); }
            set { SetValue("TypeOfEntityId", value); }
        }

        /// <inheritdoc/>
        public string? TypeOfEntityTitle
        {
            get { return GetValue("TypeOfEntityTitle"); }
            set { SetValue("TypeOfEntityTitle", value); }
        }

        /// <summary>
        /// Constructor for Domain Entity Item
        /// </summary>
        public DomainEntityItem() : base()
        {
            if (EntityId is null) { EntityId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(EntityTitle)) { EntityTitle = "(new Entity)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("EntityTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("EntityDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("TypeOfEntityId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("TypeOfEntityTitle", typeof(string)){ AllowDBNull = true},
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
