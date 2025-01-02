using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAttributeValue : IEntityAttributeItem,
        IAttributeIndex, IEntityIndex, IEntityAttributeIndex,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class EntityAttributeValue : EntityAttributeItem
    {
        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityAttribute; } }

        /// <summary>
        /// Path Index version of the AttributeName
        /// </summary>
        public PathIndex AttributePath
        {
            get
            { return new PathIndex(new PathIndex(PathIndex.Parse(AttributeName).ToArray())); }
            set
            {
                AttributeName = value.MemberFullPath;
                OnPropertyChanged(nameof(AttributePath));
            }
        }

        /// <inheritdoc/>
        public EntityAttributeValue() : base() { }

        /// <inheritdoc cref="EntityAttributeItem(IEntityKey)"/>
        public EntityAttributeValue(IEntityIndex entity) : base(entity) { }
    }
}
