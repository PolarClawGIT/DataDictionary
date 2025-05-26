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
    public interface IEntityAttributeValue : IEntityAttributeItem,
        IEntityIndex, IScopeType, ITemporal, IBindingRowState
    {
        /// <inheritdoc cref="IEntityAttributeItem.IsNullable"/>
        new Boolean? IsNullable { get; set; }

        /// <summary>
        /// Returns the Attribute Name converted to a Path.
        /// </summary>
        PathIndex AttributePath { get; set; }

        /// <summary>
        /// Returns the Attribute, if found.
        /// </summary>
        IAttributeValue? Attribute { get; }

        /// <summary>
        /// Is the Attribute associated with this Entity in the Model.
        /// </summary>
        Boolean InModel { get; }

        /// <inheritdoc cref="IAttributeKeyName.AttributeTitle"/>
        String? AttributeTitle { get; }

        /// <inheritdoc cref="DataLayer.AppModel.IAttribute.AttributeDescription"/>
        String? AttributeDescription { get; }
    }

    /// <inheritdoc/>
    public class EntityAttributeValue : EntityAttributeItem, IEntityAttributeValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <summary>
        /// The Attribute, if any, that is associated with the Entity.
        /// </summary>
        public IAttributeValue? Attribute { get { return FindAttributes(AttributePath).FirstOrDefault(); } }

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

        /// <summary>
        /// Function that finds the Attribute for the EntityAttribute
        /// </summary>
        internal FindAttributes FindAttributes
        { get; set; } = (path) => new List<IAttributeValue>();

        /// <inheritdoc/>
        public Boolean InModel
        { get { return Attribute is IAttributeValue; } }

        /// <inheritdoc/>
        public String? AttributeTitle
        { get { return Attribute is IAttributeValue value ? value.AttributeTitle : null; } }

        /// <inheritdoc/>
        public String? AttributeDescription
        { get { return Attribute is IAttributeValue value ? value.AttributeDescription : null; } }

        /// <inheritdoc/>
        public PathIndex AttributePath
        {
            get
            {
                return new PathIndex(
                    new PathIndex(PathIndex.Parse(base.AttributeName).ToArray()));
            }
            set
            {
                base.AttributeName = value.MemberFullPath;
                OnPropertyChanged(nameof(AttributePath));
            }
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

        // <summary>
        // Constructor for EntityAttributeValue
        // </summary>
        // <param name="entity"></param>
        // <param name="attribute"></param>
        //public EntityAttributeValue(IEntityIndex entity, AttributeValue attribute) : this(entity)
        //{
        //    AttributeKnownAs = attribute.AttributeTitle;
        //    AttributePath = attribute.AttributePath;
        //    FindAttribute = (path) => attribute;
        //}
    }
}
