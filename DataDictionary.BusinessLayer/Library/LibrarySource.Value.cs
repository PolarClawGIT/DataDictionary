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
        /// <inheritdoc cref="LibrarySourceItem()"/>
        public LibrarySourceValue() : base()
        { }

        /// <inheritdoc/>
        public IPathValue AsPathValue()
        {
            if (pathValue is null)
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

            return pathValue;
        }
        IPathValue? pathValue; // Backing field for AsPathValue
    }
}
