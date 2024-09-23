using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface IModelValue : IModelItem, IModelIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ModelValue : ModelItem, IModelValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="ModelItem()"/>
        public ModelValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new ModelIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return ModelTitle ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        public PathIndex GetPath()
        { return new PathIndex(ScopeEnumeration.Cast(Scope).Name); }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(ModelTitle); }
    }
}
