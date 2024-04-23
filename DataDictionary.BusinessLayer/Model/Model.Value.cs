using DataDictionary.BusinessLayer.NamedScope;
using System.ComponentModel;
using Toolbox.BindingTable;
using DbLayer = DataDictionary.DataLayer.ModelData;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface IModelValue : DbLayer.IModelItem, IModelIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ModelValue : DbLayer.ModelItem, IModelValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbLayer.ModelItem()"/>
        public ModelValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
        { return new NamedScopeKey((IModelIndex)this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(this); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return this.ModelTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(ModelTitle)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
