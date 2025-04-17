// Ignore Spelling: Nullable

using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAttributeValue : IEntityAttributeItem, IAttributeValue,
        IEntityIndex, IScopeType, ITemporal, IBindingRowState
    {
        /// <inheritdoc cref="IEntityAttributeItem.IsNullable"/>
        new Boolean? IsNullable { get; set; }

        /// <summary>
        /// Is the Attribute associated with this Entity in the Model.
        /// </summary>
        Boolean InModel { get; }
    }

    /// <inheritdoc/>
    public class EntityAttributeValue : EntityAttributeItem, IEntityAttributeValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <summary>
        /// The Attribute, if any, that is associated with the Entity.
        /// </summary>
        public IAttributeValue? Attribute
        {
            get { return attributeValue; }
            internal set
            {
                if (attributeValue is not null)
                { attributeValue.PropertyChanged -= AttributeValue_PropertyChanged; }

                attributeValue = value;

                if (attributeValue is not null)
                { attributeValue.PropertyChanged += AttributeValue_PropertyChanged; }

                void AttributeValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (!String.IsNullOrWhiteSpace(e.PropertyName))
                    { OnPropertyChanged(e.PropertyName); }
                }
            }
        }
        IAttributeValue? attributeValue;

        //public Guid? EntityId => throw new NotImplementedException();
        //public String? AttributePath => throw new NotImplementedException();
        //public Boolean? IsNullable => throw new NotImplementedException();
        //public Boolean? IsPrimaryKey => throw new NotImplementedException();
        //public Int32? OrdinalPosition => throw new NotImplementedException();

        /// <inheritdoc/>
        public DataIndex Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        public String Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityAttribute; } }

        /// <inheritdoc/>
        public Boolean InModel { get { return attributeValue is not null; } }

        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.AttributeId; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String? AttributeTitle
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.AttributeTitle; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String? AttributeDescription
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.AttributeDescription; }
                else { return null; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.AttributeDescription = value; }
            }
        }

        /// <inheritdoc/>
        public String? DataType
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.DataType; }
                else { return null; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.DataType = value; }
            }
        }

        /// <inheritdoc/>
        public Int16? DataLength
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.DataLength; }
                else { return null; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.DataLength = value; }
            }
        }

        /// <inheritdoc/>
        public Byte? DataPrecision
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.DataPrecision; }
                else { return null; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.DataPrecision = value; }
            }
        }

        /// <inheritdoc/>
        public Byte? DataScale
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.DataScale; }
                else { return null; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.DataScale = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSingleValue
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsSingleValue; }
                else { return false; ; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsSingleValue = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsMultiValue
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsMultiValue; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsMultiValue = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSimpleType
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsSimpleType; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsSimpleType = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCompositeType
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsCompositeType; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsCompositeType = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsDerived
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsDerived; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsDerived = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsIntegral
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsIntegral; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsIntegral = value; }
            }
        }

        /// <inheritdoc/>
        public new Boolean? IsNullable
        {
            get { return base.IsNullable; }
            set { base.IsNullable = value; }
        }

        /// <inheritdoc/>
        Boolean DataLayer.AppModel.IAttribute.IsNullable
        {
            get
            {
                if (attributeValue is not null)
                { return attributeValue.IsNullable; }
                else { return false; }
            }

            set
            {
                if (attributeValue is not null)
                { attributeValue.IsNullable = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsValued
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsValued; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsValued = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsKey
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsKey; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsKey = value; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNonKey
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.IsNonKey; }
                else { return false; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.IsNonKey = value; }
            }
        }

        /// <inheritdoc/>
        public String? AttributeName
        {
            get
            {
                if (Attribute is not null)
                { return Attribute.AttributeName; }
                else { return null; }
            }
            set
            {
                if (Attribute is not null)
                { Attribute.AttributeName = value; }
            }
        }

        /// <inheritdoc cref="EntityAttributeItem.AttributePath"/>
        public new PathIndex AttributePath
        {
            get
            {
                return new PathIndex(
                    new PathIndex(PathIndex.Parse(base.AttributePath).ToArray()));
            }
            set { base.AttributePath = value.MemberFullPath; }
        }

        /// <inheritdoc/>
        public EntityAttributeValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new EntityIndex(this),
                GetTitle = () => AttributeKnownAs ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(AttributeKnownAs)
            };
        }

        /// <inheritdoc cref="EntityAttributeItem(IEntityKey)"/>
        public EntityAttributeValue(IEntityIndex entity) : base(entity)
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new EntityIndex(this),
                GetTitle = () => AttributeKnownAs ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(AttributeKnownAs)
            };
        }

        /// <summary>
        /// Constructor for EntityAttributeValue
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        public EntityAttributeValue(IEntityIndex entity, AttributeValue attribute) : this(entity)
        {   
            Attribute = attribute;
            AttributeKnownAs = attribute.AttributeTitle;
            AttributePath = attribute.AttributePath;
        }
    }
}
