using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppLibrary;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppLibrary
{
    /// <inheritdoc/>
    public interface ILibrarySourceIndex : ILibrarySourceKey
    { }

    /// <inheritdoc/>
    public class LibrarySourceIndex : LibrarySourceKey, ILibrarySourceIndex,
        IKeyEquality<ILibrarySourceIndex>, IKeyEquality<LibrarySourceIndex>,
        IAuthorization
    {
        /// <inheritdoc cref="LibrarySourceKey(ILibrarySourceKey)"/>
        public LibrarySourceIndex(ILibrarySourceIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ILibrarySourceIndex? other)
        { return other is ILibrarySourceKey key && Equals(new LibrarySourceKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(LibrarySourceIndex? other)
        { return other is ILibrarySourceKey key && Equals(new LibrarySourceKey(key)); }

        /// <summary>
        /// Convert LibrarySourceIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(LibrarySourceIndex source)
        { return new DataIndex() { SystemId = source.LibraryId ?? Guid.Empty }; }

        /// <inheritdoc/>
        public (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
        {
            if (HasValue)
            {
                return (
                IsAdmin: authorizations.IsLibraryAdmin,
                IsOwner: authorizations.IsLibraryOwner,
                IsGrant: authorizations.IsGrant(new SecurableIndex() { SecurableId = LibraryId }));
            }
            else { return (false, false, false); }
        }
    }
}
