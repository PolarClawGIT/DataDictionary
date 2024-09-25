using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceValue : ILibrarySourceItem, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibrarySourceValue : LibrarySourceItem, ILibrarySourceValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public LibrarySourceValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new LibrarySourceIndex(this),
                GetPath = () => new PathIndex(AssemblyName),
                GetScope = () => Scope,
                GetTitle = () => LibraryTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(AssemblyName),
                IsTitleChanged = (e) => e.PropertyName is nameof(LibraryTitle)
            };
        }
    }
}
