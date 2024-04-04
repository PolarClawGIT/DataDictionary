using DataDictionary.DataLayer.DomainData.Alias;
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
    public interface ILibraryMemberKeyName : IKey, IToAliasName
    {
        /// <summary>
        /// NameSpace that the Member is within
        /// </summary>
        String? MemberNameSpace { get; }

        /// <summary>
        /// Name of the Member.
        /// </summary>
        String? MemberName { get; }
    }

    /// <summary>
    /// Implementation for ILibraryMemberKeyName
    /// </summary>
    public static class LibraryMemberKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Library Member.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this ILibraryMemberKeyName source)
        {
            if (String.IsNullOrWhiteSpace(source.MemberNameSpace))
            { return AliasExtension.FormatName(source.MemberName); }
            else
            { return AliasExtension.FormatName(String.Format("{0}.{1}", source.MemberNameSpace, source.MemberName)); }
        }
    }

    /// <summary>
    /// Implementation of the Library Member Alternate Key
    /// </summary>
    public class LibraryMemberKeyName : ILibraryMemberKeyName, IKeyComparable<ILibraryMemberKeyName>
    {
        /// <inheritdoc/>
        public String MemberNameSpace { get; init; } = String.Empty;

        /// <inheritdoc/>
        public String MemberName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Library Member Alternate Key
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberKeyName(ILibraryMemberKeyName source) : base()
        {
            if (source.MemberNameSpace is String) { MemberNameSpace = source.MemberNameSpace; }
            if (source.MemberName is String) { MemberName = source.MemberName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(ILibraryMemberKeyName? other)
        {
            return
                other is ILibraryMemberKeyName &&
                !string.IsNullOrEmpty(MemberNameSpace) &&
                !string.IsNullOrEmpty(other.MemberNameSpace) &&
                !string.IsNullOrEmpty(MemberName) &&
                !string.IsNullOrEmpty(other.MemberName) &&
                MemberNameSpace.Equals(other.MemberNameSpace, KeyExtension.CompareString) &&
                MemberName.Equals(other.MemberName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryMemberKeyName value && Equals(new LibraryMemberKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(ILibraryMemberKeyName? other)
        {
            if (other is null) { return 1; }
            else if (String.Compare(MemberNameSpace, other.MemberNameSpace, KeyExtension.CompareString) is int value && value != 0) { return value; }
            else { return String.Compare(MemberName, other.MemberName, KeyExtension.CompareString); }
        }

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        { if (obj is ILibraryMemberKeyName value) { return CompareTo(new LibraryMemberKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(LibraryMemberKeyName left, LibraryMemberKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryMemberKeyName left, LibraryMemberKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(LibraryMemberKeyName left, LibraryMemberKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(LibraryMemberKeyName left, LibraryMemberKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(LibraryMemberKeyName left, LibraryMemberKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(LibraryMemberKeyName left, LibraryMemberKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(MemberNameSpace, MemberName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }

    }
}
