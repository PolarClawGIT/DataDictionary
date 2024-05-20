using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ModelData;
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
        { return ModelTitle ?? Scope.ToName(); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(Scope.ToName()); }
    }
}
