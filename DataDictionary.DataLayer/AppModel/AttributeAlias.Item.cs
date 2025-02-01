using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Attribute Alias Items
    /// </summary>
    public interface IAttributeAliasItem : IAttributeKey, IAliasKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Attribute Alias Items
    /// </summary>
    [Serializable]
    public class AttributeAliasItem : BindingTableRow, IAttributeAliasItem
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public String? AliasPath { get { return GetValue(nameof(AliasPath)); } set { SetValue(nameof(AliasPath), value); } }

        /// <inheritdoc/>
        public ScopeType AliasScope
        {
            get
            {
                String value = GetValue(nameof(AliasScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(AliasScope), null); }
                else { SetValue(nameof(AliasScope), ScopeEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AliasScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AliasPath), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        public AttributeAliasItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="key"></param>
        public AttributeAliasItem(IAttributeKey key) : this()
        { AttributeId = key.AttributeId; }


        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AttributeAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        {
            if (AliasPath is String) { return AliasPath; }
            else { return String.Empty; }
        }

    }
}
