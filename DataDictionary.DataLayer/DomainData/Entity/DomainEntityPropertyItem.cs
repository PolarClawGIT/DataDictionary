using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Properties
    /// </summary>
    public interface IDomainEntityPropertyItem : IDomainEntityPropertyKey, IDataItem
    {
        /// <summary>
        /// Property Value. Type and Format is dependent on PeropertyId.
        /// </summary>
        public string? PropertyValue { get; }

        /// <summary>
        /// Property Value for Rich Text Definition properties.
        /// </summary>
        public string? DefinitionText { get; }
    }

    /// <summary>
    /// Implementation for Domain Entity Properties
    /// </summary>
    [Serializable]
    public class DomainEntityPropertyItem : BindingTableRow, IDomainEntityPropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId { get { return GetValue<Guid>("EntityId"); } protected set { SetValue("EntityId", value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>("PropertyId"); } set { SetValue("PropertyId", value); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue("PropertyValue"); } set { SetValue("PropertyValue", value); } }

        /// <inheritdoc/>
        public string? DefinitionText { get { return GetValue("DefinitionText"); } set { SetValue("DefinitionText", value); } }

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
        public DomainEntityPropertyItem(IDomainEntityKey EntityKey, IPropertyKey propertyKey, IDbExtendedPropertyItem value) : this()
        {
            EntityId = EntityKey.EntityId;
            PropertyId = propertyKey.PropertyId;
            PropertyValue = value.PropertyValue;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyValue", typeof(string)){ AllowDBNull = true},
            new DataColumn("DefinitionText", typeof(string)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
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
