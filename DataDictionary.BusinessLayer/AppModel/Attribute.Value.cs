using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeValue : IAttributeItem, 
        IAttributeIndex, IAttributeIndexName,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public partial class AttributeValue : AttributeItem, IAttributeValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttribute; } }

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
        public AttributeValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new AttributeIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(AttributeName))
                    { return new PathIndex(AttributeTitle); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(AttributeName).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => AttributeTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(AttributeTitle) or nameof(AttributeName),
                IsTitleChanged = (e) => e.PropertyName is nameof(AttributeTitle)
            };
        }

    }
}
