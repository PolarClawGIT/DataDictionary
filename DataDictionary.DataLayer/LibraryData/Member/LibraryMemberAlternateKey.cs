using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library Member Alternate Key
    /// </summary>
    public interface ILibraryMemberAlternateKey : IKey
    {
        /// <summary>
        /// NameSpace that the Member is within
        /// </summary>
        string? NameSpace { get; }

        /// <summary>
        /// Name of the Member.
        /// </summary>
        string? MemberName { get; }
    }

    /// <summary>
    /// Implementation of the Library Member Alternate Key
    /// </summary>
    public class LibraryMemberAlternateKey : ILibraryMemberAlternateKey, IKeyComparable<ILibraryMemberAlternateKey>
    {
        /// <inheritdoc/>
        public string NameSpace { get; init; } = String.Empty;

        /// <inheritdoc/>
        public string MemberName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Library Member Alternate Key
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberAlternateKey(ILibraryMemberAlternateKey source) : base()
        {
            if (source.NameSpace is String) { NameSpace = source.NameSpace; }
            if (source.MemberName is String) { MemberName = source.MemberName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(ILibraryMemberAlternateKey? other)
        {
            return
                other is ILibraryMemberAlternateKey &&
                !string.IsNullOrEmpty(NameSpace) &&
                !string.IsNullOrEmpty(other.NameSpace) &&
                !string.IsNullOrEmpty(MemberName) &&
                !string.IsNullOrEmpty(other.MemberName) &&
                NameSpace.Equals(other.NameSpace, KeyExtension.CompareString) &&
                MemberName.Equals(other.MemberName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryMemberAlternateKey value && Equals(new LibraryMemberAlternateKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(ILibraryMemberAlternateKey? other)
        {
            if (other is null) { return 1; }
            else if (String.Compare(NameSpace, other.NameSpace, KeyExtension.CompareString) is int value && value != 0) { return value; }
            else { return String.Compare(MemberName, other.MemberName, KeyExtension.CompareString); }
        }

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        { if (obj is ILibraryMemberAlternateKey value) { return CompareTo(new LibraryMemberAlternateKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(LibraryMemberAlternateKey left, LibraryMemberAlternateKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryMemberAlternateKey left, LibraryMemberAlternateKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(LibraryMemberAlternateKey left, LibraryMemberAlternateKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(LibraryMemberAlternateKey left, LibraryMemberAlternateKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(LibraryMemberAlternateKey left, LibraryMemberAlternateKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(LibraryMemberAlternateKey left, LibraryMemberAlternateKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(NameSpace, MemberName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (NameSpace is string && MemberName is String)
            { return string.Format("{0}.{1}", NameSpace, MemberName); }
            else { return string.Empty; }
        }
    }
}
