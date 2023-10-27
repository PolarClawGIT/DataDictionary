using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library NameSpace Key
    /// </summary>
    public interface ILibraryNameSpaceKey : ILibrarySourceKey
    {
        /// <summary>
        /// NameSpace for the Member.
        /// </summary>
        string? MemberNameSpace { get; }

    }

    /// <summary>
    /// Implementation of the Library NameSpace Key
    /// </summary>
    public class LibraryNameSpaceKey : ILibraryNameSpaceKey, IKeyEquality<ILibraryNameSpaceKey>
    {
        /// <inheritdoc/>
        public Guid? LibraryId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public string MemberNameSpace { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Library NameSpace Key
        /// </summary>
        /// <param name="source"></param>
        private LibraryNameSpaceKey(ILibraryNameSpaceKey source) 
        {
            if (source.MemberNameSpace is string) { MemberNameSpace = source.MemberNameSpace; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(ILibraryNameSpaceKey? other)
        {
            return other is ILibraryNameSpaceKey &&
                new LibrarySourceKey(this).Equals(other) &&
                !string.IsNullOrEmpty(MemberNameSpace) &&
                !string.IsNullOrEmpty(other.MemberNameSpace) &&
                MemberNameSpace.Equals(other.MemberNameSpace, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryNameSpaceKey value && Equals(new LibraryNameSpaceKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibraryNameSpaceKey left, LibraryNameSpaceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryNameSpaceKey left, LibraryNameSpaceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(LibraryId, MemberNameSpace); }
        #endregion
    }
}
