using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
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
        { return SubjectAreaTitle ?? ScopeEnumeration.Cast(Scope).Name; ; }

        /// <inheritdoc/>
        public PathIndex GetPath()
        { return new PathIndex(new PathIndex(PathIndex.Parse(this.SubjectName).ToArray())); }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(SubjectName) or nameof(SubjectAreaTitle); }
    }
}
