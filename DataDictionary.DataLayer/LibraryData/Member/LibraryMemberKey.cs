using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.LibraryData.Source;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library Member Key
    /// </summary>
    public interface ILibraryMemberKey : ILibrarySourceKey
    {
        /// <summary>
        /// NameSpace for the Member.
        /// </summary>
        string? MemberNameSpace { get; }

        /// <summary>
        /// Name of the Member.
        /// </summary>
        string? MemberName { get; }
    }

    /// <summary>
    /// Implementation of the Library Member Key
    /// </summary>
    public class LibraryMemberKey : LibrarySourceKey, ILibraryMemberKey, IKeyEquality<ILibraryMemberKey>
    {
        /// <inheritdoc/>
        public string MemberNameSpace { get; init; } = string.Empty;

        /// <inheritdoc/>
        public string MemberName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Library Member Key
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKey(ILibraryMemberKey source) : base(source)
        {
            if (source.MemberNameSpace is string) { MemberNameSpace = source.MemberNameSpace; }
            if (source.MemberName is string) { MemberName = source.MemberName; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(ILibraryMemberKey? other)
        {
            return other is ILibraryMemberKey &&
                new LibrarySourceKey(this).Equals(other) &&
                !string.IsNullOrEmpty(MemberNameSpace) &&
                !string.IsNullOrEmpty(other.MemberNameSpace) &&
                !string.IsNullOrEmpty(MemberName) &&
                !string.IsNullOrEmpty(other.MemberName) &&
                MemberNameSpace.Equals(other.MemberNameSpace, KeyExtension.CompareString) &&
                MemberName.Equals(other.MemberName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryMemberKey value && Equals(new LibraryMemberKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibraryMemberKey left, LibraryMemberKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryMemberKey left, LibraryMemberKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(LibraryId, MemberNameSpace, MemberName); }
        #endregion

    }
}
