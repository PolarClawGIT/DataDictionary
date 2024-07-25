using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Source
{
    /// <summary>
    /// Interface for the Library Source Unique Key
    /// </summary>
    public interface ILibrarySourceKeyName : IKey, IToAliasName
    {
        /// <summary>
        /// Assembly Name for the Library Source
        /// </summary>
        String? AssemblyName { get; }
    }

    /// <summary>
    /// Implementation for ILibrarySourceKeyName
    /// </summary>
    public static class LibrarySourceKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Library Member.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this ILibrarySourceKeyName source)
        { return AliasExtension.FormatName(source.AssemblyName); }
    }

    /// <summary>
    /// Implementation of the Library Source Unique Key 
    /// </summary>
    public class LibrarySourceKeyName : ILibrarySourceKeyName,
        IKeyComparable<ILibrarySourceKeyName>, IKeyComparable<LibrarySourceKeyName>
    {
        /// <inheritdoc/>
        public string AssemblyName { get; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Alias
        /// </summary>
        /// <param name="source"></param>
        public LibrarySourceKeyName(ILibrarySourceKeyName source) : base()
        { if (source.AssemblyName is string) { AssemblyName = source.AssemblyName; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(LibrarySourceKeyName? other)
        {
            return
                other is LibrarySourceKeyName &&
                !string.IsNullOrEmpty(AssemblyName) &&
                !string.IsNullOrEmpty(other.AssemblyName) &&
                AssemblyName.Equals(other.AssemblyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(ILibrarySourceKeyName? other)
        { return other is ILibrarySourceKeyName value && Equals(new LibrarySourceKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ILibrarySourceKeyName value && Equals(new LibrarySourceKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(LibrarySourceKeyName? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(AssemblyName, other.AssemblyName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(ILibrarySourceKeyName? other)
        { if (other is ILibrarySourceKeyName value) { return CompareTo(new LibrarySourceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public Int32 CompareTo(object? obj)
        { if (obj is ILibrarySourceKeyName value) { return CompareTo(new LibrarySourceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(LibrarySourceKeyName left, LibrarySourceKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(LibrarySourceKeyName left, LibrarySourceKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(LibrarySourceKeyName left, LibrarySourceKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(LibrarySourceKeyName left, LibrarySourceKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(LibrarySourceKeyName left, LibrarySourceKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(LibrarySourceKeyName left, LibrarySourceKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(AssemblyName); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (AssemblyName is string)
            { return AssemblyName; }
            else { return string.Empty; }
        }
    }
}
