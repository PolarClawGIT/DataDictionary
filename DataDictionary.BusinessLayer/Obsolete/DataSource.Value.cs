using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface IDataSourceValue : IDataSourceItem, IDataSourceIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class DataSourceValue : DataSourceItem, IDataSourceValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        public DataSourceValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DataSourceIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => DataSourceTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DataSourceTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DataSourceTitle)
            };
        }
    }
}
