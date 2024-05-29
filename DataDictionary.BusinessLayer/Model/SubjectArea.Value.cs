using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
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
    public class SubjectAreaValue : ModelSubjectAreaItem, ISubjectAreaValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="ModelSubjectAreaItem()"/>
        public SubjectAreaValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new SubjectAreaIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return SubjectAreaTitle ?? Scope.ToName(); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(new NamedScopePath(NamedScopePath.Parse(this.SubjectAreaNameSpace).ToArray())); }
    }
}
