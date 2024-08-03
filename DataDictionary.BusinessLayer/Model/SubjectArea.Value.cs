using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource.Enumerations;
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
        public NamedScopePath GetPath()
        { return new NamedScopePath(new NamedScopePath(NamedScopePath.Parse(this.SubjectName).ToArray())); }
    }
}
