using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Primary Key for the LibraryMember.
    /// </summary>
    public interface ILibraryMemberKey : ILibraryAssemblyKey
    {
        /// <summary>
        /// Member Id, GUID assigned by the application. This is the Primary Key.
        /// </summary>
        Nullable<Guid> MemberId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key for LibraryMember.
    /// </summary>
    public class LibraryMemberKey : LibraryAssemblyKey, ILibraryMemberKey, IEquatable<LibraryMemberKey>
    {
        /// <inheritdoc/>
        public Nullable<Guid> MemberId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for LibraryMemberKey
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKey(ILibraryMemberKey source) : base(source)
        {
            if (source.MemberId is Guid) { MemberId = source.MemberId; }
            else { MemberId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(LibraryMemberKey? other)
        { return other is ILibraryMemberKey key
                && EqualityComparer<Guid?>.Default.Equals(AssemblyId, key.AssemblyId)
                && EqualityComparer<Guid?>.Default.Equals(MemberId, key.MemberId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryMemberKey value && this.Equals(new LibraryMemberKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibraryMemberKey left, LibraryMemberKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryMemberKey left, LibraryMemberKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(MemberId); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (MemberId is Guid && MemberId.ToString() is String value) { return value; }
            else { return String.Empty; }
        }
    }
}
