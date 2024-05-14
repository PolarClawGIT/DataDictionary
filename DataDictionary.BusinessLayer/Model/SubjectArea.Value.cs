using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface ISubjectAreaValue : IModelSubjectAreaItem, ISubjectAreaIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class SubjectAreaValue : ModelSubjectAreaItem, ISubjectAreaValue, INamedScopeSource
    {
        /// <inheritdoc cref="ModelSubjectAreaItem()"/>
        public SubjectAreaValue() : base()
        { PropertyChanged += SubjectAreaValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeIndex GetKey()
        { return new NamedScopeIndex(SubjectAreaId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(NamedScopePath.Parse(SubjectAreaNameSpace).ToArray()); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SubjectAreaTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void SubjectAreaValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SubjectAreaTitle)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
