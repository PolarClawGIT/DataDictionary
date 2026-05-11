using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IModelValue : IModelItem, IModelIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType
    { }

    /// <inheritdoc/>
    public class ModelValue : ModelItem, IModelValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.Model; } }

        /// <inheritdoc/>
        public ModelValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new ModelIndex(this),
                GetPath = () => new PathIndex(ModelTitle),
                GetScope = () => Scope,
                GetTitle = () => ModelTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ModelTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(ModelTitle)
            };
        }

        /// <inheritdoc/>
        public (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
        { return new ModelIndex(this).GetAuthorization(authorizations); }
    }
}
