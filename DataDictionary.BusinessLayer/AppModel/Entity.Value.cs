using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityValue : IEntityItem, IEntityIndex, IEntityIndexName,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public partial class EntityValue : EntityItem, IEntityValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntity; } }

        /// <summary>
        /// Path Index version of the EntityName
        /// </summary>
        public PathIndex EntityPath
        {
            get
            { return new PathIndex(new PathIndex(PathIndex.Parse(EntityName).ToArray())); }
            set
            {
                EntityName = value.MemberFullPath;
                OnPropertyChanged(nameof(EntityPath));
            }
        }

        /// <inheritdoc/>
        public EntityValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new EntityIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(EntityName))
                    { return new PathIndex(EntityTitle); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(EntityName).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => EntityTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(EntityTitle) or nameof(EntityName),
                IsTitleChanged = (e) => e.PropertyName is nameof(EntityTitle)
            };
        }
    }
}
