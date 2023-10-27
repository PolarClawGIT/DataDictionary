using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library Parent Member Key
    /// </summary>
    public interface ILibraryMemberKeyParent : ILibrarySourceKey
    {
        /// <summary>
        /// Parent Member Id for the Library Member
        /// </summary>
        public Guid? ParentMemberId { get; }
    }

    /// <summary>
    /// Implementation for the Library Parent Member Key
    /// </summary>
    public class LibraryMemberKeyParent : LibrarySourceKey, ILibraryMemberKeyParent, IKeyEquality<ILibraryMemberKeyParent>, IKeyEquality<ILibraryMemberKey>
    {
        /// <inheritdoc/>
        public Guid? ParentMemberId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Library Member Key
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKeyParent(ILibraryMemberKeyParent source) : base(source)
        {
            if (source.ParentMemberId is Guid) { ParentMemberId = source.ParentMemberId; }
            else { ParentMemberId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(ILibraryMemberKeyParent? other)
        {
            return other is ILibraryMemberKey &&
                new LibrarySourceKey(this).Equals(other) &&
                EqualityComparer<Guid?>.Default.Equals(ParentMemberId, other.ParentMemberId);
        }

        /// <inheritdoc/>
        public bool Equals(ILibraryMemberKey? other)
        {
            return other is ILibraryMemberKey &&
                new LibrarySourceKey(this).Equals(other) &&
                EqualityComparer<Guid?>.Default.Equals(ParentMemberId, other.MemberId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryMemberKeyParent value && Equals(new LibraryMemberKeyParent(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibraryMemberKeyParent left, LibraryMemberKeyParent right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryMemberKeyParent left, LibraryMemberKeyParent right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(LibraryId, ParentMemberId); }
        #endregion

    }
}
