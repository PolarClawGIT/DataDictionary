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
        /// Member Id for the Library Member
        /// </summary>
        Guid? MemberId { get; }
    }

    /// <summary>
    /// Implementation of the Library Member Key
    /// </summary>
    public class LibraryMemberKey : LibrarySourceKey, ILibraryMemberKey, IKeyEquality<ILibraryMemberKey>
    {
        /// <inheritdoc/>
        public Guid? MemberId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public override Boolean HasValue { get { return base.HasValue && MemberId.HasValue && MemberId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Library Member Key
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKey(ILibraryMemberKey source) : base(source)
        {
            if (source.MemberId is Guid) { MemberId = source.MemberId; }
            else { MemberId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Library Member Key
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKey(ILibraryMemberKeyParent source) : base(source)
        {
            if (source.MemberParentId is Guid) { MemberId = source.MemberParentId; }
            else { MemberId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(ILibraryMemberKey? other)
        {
            return other is ILibraryMemberKey &&
                new LibrarySourceKey(this).Equals(other) &&
                EqualityComparer<Guid?>.Default.Equals(MemberId, other.MemberId);
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
        { return HashCode.Combine(LibraryId, MemberId); }
        #endregion
    }
}
