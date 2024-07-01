using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Item
    /// </summary>
    public interface IDomainEntityItem : IDomainEntityKey, IDomainEntityKeyName, IScopeKey
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
        public ScopeType Scope { get { return ScopeType.ModelEntity; } }

        /// <inheritdoc/>
        public Guid? TypeOfEntityId
        {
            get { return GetValue<Guid>(nameof(TypeOfEntityId)); }
            set { SetValue(nameof(TypeOfEntityId), value); }
        }

        /// <inheritdoc/>
        public String? TypeOfEntityTitle
        {
            get { return GetValue(nameof(TypeOfEntityTitle)); }
            set { SetValue(nameof(TypeOfEntityTitle), value); }
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
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(EntityTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(EntityDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(TypeOfEntityId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TypeOfEntityTitle), typeof(String)){ AllowDBNull = true},
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
        public override String ToString()
        { if (EntityTitle is not null) { return EntityTitle; } else { return string.Empty; } }
    }
}
