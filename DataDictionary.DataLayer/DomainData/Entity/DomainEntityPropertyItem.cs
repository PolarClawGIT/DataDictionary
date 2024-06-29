using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Properties
    /// </summary>
    public interface IDomainEntityPropertyItem : IDomainEntityPropertyKey, IDomainProperty, IScopeKey
    { }

    /// <summary>
    /// Implementation for Domain Entity Properties
    /// </summary>
    [Serializable]
    public class DomainEntityPropertyItem : BindingTableRow, IDomainEntityPropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId { get { return GetValue<Guid>(nameof(EntityId)); } protected set { SetValue(nameof(EntityId), value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>(nameof(PropertyId)); } set { SetValue(nameof(PropertyId), value); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue(nameof(PropertyValue)); } set { SetValue(nameof(PropertyValue), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelEntityProperty;

        /// <summary>
        /// Constructor for Domain Entity Properties
        /// </summary>
        public DomainEntityPropertyItem() : base() { }

        /// <summary>
        /// Constructor for Domain Entity Properties
        /// </summary>
        /// <param name="EntityKey"></param>
        public DomainEntityPropertyItem(IDomainEntityKey EntityKey) : this()
        { EntityId = EntityKey.EntityId; }

        /// <summary>
        /// Constructor for Domain Entity Properties
        /// </summary>
        /// <param name="EntityKey"></param>
        /// <param name="propertyKey"></param>
        /// <param name="value"></param>
        public DomainEntityPropertyItem(IDomainEntityKey EntityKey, IDomainPropertyKey propertyKey, IDbExtendedPropertyItem value) : this()
        {
            EntityId = EntityKey.EntityId;
            PropertyId = propertyKey.PropertyId;
            PropertyValue = value.PropertyValue;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Properties
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntityPropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
