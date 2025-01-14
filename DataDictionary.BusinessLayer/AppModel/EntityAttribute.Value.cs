using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAttributeValue : IEntityAttributeItem,
        IEntityIndex, IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class EntityAttributeValue : EntityAttributeItem, IEntityAttributeValue
    {
        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityAttribute; } }

        /// <inheritdoc cref="EntityAttributeItem.AttributePath"/>
        public new PathIndex AttributePath
        {
            get
            { return new PathIndex(new PathIndex(PathIndex.Parse(base.AttributePath).ToArray())); }
            set
            {
                base.AttributePath = value.MemberFullPath;
                OnPropertyChanged(nameof(AttributePath));
            }
        }

        /// <inheritdoc/>
        public EntityAttributeValue() : base() { }

        /// <inheritdoc cref="EntityAttributeItem(IEntityKey)"/>
        public EntityAttributeValue(IEntityIndex entity) : base(entity) { }
    }
}
