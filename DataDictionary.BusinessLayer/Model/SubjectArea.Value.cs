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
    public class SubjectAreaValue : ModelSubjectAreaItem, ISubjectAreaValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public SubjectAreaValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SubjectAreaIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(SubjectName))
                    { return new PathIndex(SubjectAreaTitle); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(SubjectName).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => SubjectAreaTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(SubjectAreaTitle) or nameof(SubjectName),
                IsTitleChanged = (e) => e.PropertyName is nameof(SubjectAreaTitle)
            };
        }
    }
}
