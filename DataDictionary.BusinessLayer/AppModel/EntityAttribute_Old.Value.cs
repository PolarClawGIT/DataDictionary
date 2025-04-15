using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    [Obsolete]
    public interface IEntityAttributeValue_Old : IEntityAttributeItem,
        IEntityIndex, IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class EntityAttributeValue_Old : EntityAttributeItem, IEntityAttributeValue_Old
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
        public EntityAttributeValue_Old() : base() { }

        /// <inheritdoc cref="EntityAttributeItem(IEntityKey)"/>
        public EntityAttributeValue_Old(IEntityIndex entity) : base(entity) { }
    }
}
