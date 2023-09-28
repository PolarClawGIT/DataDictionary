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
    [Obsolete]
    public interface ILibraryMemberKey_Old : ILibraryAssemblyKey_old
    {
        /// <summary>
        /// Member Id, GUID assigned by the application. This is the Primary Key.
        /// </summary>
        Nullable<Guid> MemberId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key for LibraryMember.
    /// </summary>
    [Obsolete]
    public class LibraryMemberKey_Old : LibraryAssemblyKey_Old, ILibraryMemberKey_Old, IEquatable<LibraryMemberKey_Old>
    {
        /// <inheritdoc/>
        public Nullable<Guid> MemberId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for LibraryMemberKey
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKey_Old(ILibraryMemberKey_Old source) : base(source)
        {
            if (source.MemberId is Guid) { MemberId = source.MemberId; }
            else { MemberId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(LibraryMemberKey_Old? other)
        { return other is ILibraryMemberKey_Old key
                && EqualityComparer<Guid?>.Default.Equals(AssemblyId, key.AssemblyId)
                && EqualityComparer<Guid?>.Default.Equals(MemberId, key.MemberId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryMemberKey_Old value && this.Equals(new LibraryMemberKey_Old(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibraryMemberKey_Old left, LibraryMemberKey_Old right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryMemberKey_Old left, LibraryMemberKey_Old right)
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
