using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppLibrary;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberValue : ILibraryMemberItem, ILibraryMemberIndex, ILibraryMemberIndexParent, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibraryMemberValue : LibraryMemberItem, ILibraryMemberValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (MemberType)
                {
                    case LibraryMemberType.NameSpace: return ScopeType.LibraryNameSpace;
                    case LibraryMemberType.Type: return ScopeType.LibraryType;
                    case LibraryMemberType.Field: return ScopeType.LibraryTypeField;
                    case LibraryMemberType.Property: return ScopeType.LibraryTypeProperty;
                    case LibraryMemberType.Method: return ScopeType.LibraryTypeMethod;
                    case LibraryMemberType.Event: return ScopeType.LibraryTypeEvent;
                    case LibraryMemberType.Parameter: return ScopeType.LibraryTypeParameter;
                    default: return ScopeType.Null;
                }
            }
        }

        /// <inheritdoc/>
        public LibraryMemberValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new LibraryMemberIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(MemberNameSpace).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => MemberName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(MemberName) or nameof(MemberNameSpace),
                IsTitleChanged = (e) => e.PropertyName is nameof(MemberName)
            };
        }
    }
}
