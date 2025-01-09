// Ignore Spelling: Nullable

using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Domain Attribute Item
    /// </summary>
    public interface IAttributeItem : IAttributeKey, IAttribute,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Domain Attribute Item
    /// </summary>
    [Serializable]
    public class AttributeItem : BindingTableRow, IAttributeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get { return GetValue<Guid>(nameof(AttributeId)); }
            protected set { SetValue(nameof(AttributeId), value); }
        }

        /// <inheritdoc/>
        public String? AttributeTitle
        {
            get { return GetValue(nameof(AttributeTitle)); }
            set { SetValue(nameof(AttributeTitle), value); }
        }

        /// <inheritdoc/>
        public String? AttributeDescription
        {
            get { return GetValue(nameof(AttributeDescription)); }
            set { SetValue(nameof(AttributeDescription), value); }
        }

        /// <inheritdoc/>
        public String? AttributeName
        {
            get { return GetValue(nameof(AttributeName)); }
            set { SetValue(nameof(AttributeName), value); }
        }

        /// <inheritdoc/>
        public String? DataType
        {
            get { return GetValue(nameof(DataType)); }
            set { SetValue(nameof(DataType), value); }
        }

        /// <inheritdoc/>
        public Int16? DataLength
        {
            get { return GetValue<Int16>(nameof(DataLength)); }
            set { SetValue(nameof(DataLength), value); }
        }

        /// <inheritdoc/>
        public Byte? DataPrecision
        {
            get { return GetValue<Byte>(nameof(DataPrecision)); }
            set { SetValue(nameof(DataPrecision), value); }
        }


        /// <inheritdoc/>
        public Boolean IsSingleValue
        {
            get
            {
                if (GetValue<bool>(nameof(IsSingleValue), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsSingleValue), value);
                if (value == true) { SetValue<Boolean>(nameof(IsMultiValue), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsMultiValue
        {
            get
            {
                if (GetValue<bool>(nameof(IsMultiValue), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsMultiValue), value);
                if (value == true) { SetValue<Boolean>(nameof(IsSingleValue), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSimpleType
        {
            get
            {
                if (GetValue<bool>(nameof(IsSimpleType), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsSimpleType), value);
                if (value == true) { SetValue<Boolean>(nameof(IsCompositeType), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCompositeType
        {
            get
            {
                if (GetValue<bool>(nameof(IsCompositeType), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsCompositeType), value);
                if (value == true) { SetValue<Boolean>(nameof(IsSimpleType), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsIntegral
        {
            get
            {
                if (GetValue<bool>(nameof(IsIntegral), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsIntegral), value);
                if (value == true) { SetValue<Boolean>(nameof(IsDerived), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsDerived
        {
            get
            {
                if (GetValue<bool>(nameof(IsDerived), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsDerived), value);
                if (value == true) { SetValue<Boolean>(nameof(IsIntegral), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsValued
        {
            get
            {
                if (GetValue<bool>(nameof(IsValued), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsValued), value);
                if (value == true) { SetValue<Boolean>(nameof(IsNullable), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNullable
        {
            get
            {
                if (GetValue<bool>(nameof(IsNullable), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsNullable), value);
                if (value == true) { SetValue<Boolean>(nameof(IsValued), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsKey
        {
            get
            {
                if (GetValue<bool>(nameof(IsKey), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsKey), value);
                if (value == true) { SetValue<Boolean>(nameof(IsNonKey), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNonKey
        {
            get
            {
                if (GetValue<bool>(nameof(IsNonKey), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsNonKey), value);
                if (value == true) { SetValue<Boolean>(nameof(IsKey), !value); }
            }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Domain Attribute Item
        /// </summary>
        public AttributeItem() : base()
        {
            if (AttributeId is null) { AttributeId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(AttributeTitle)) { AttributeTitle = "(new Attribute)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }


        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AttributeName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DataLength), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(DataPrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(IsSingleValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsMultiValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsSimpleType), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCompositeType), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsIntegral), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDerived), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsValued), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsNullable), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsKey), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsNonKey), typeof(Boolean)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        public override string ToString()
        { if (AttributeTitle is not null) { return AttributeTitle; } else { return string.Empty; } }
    }
}
