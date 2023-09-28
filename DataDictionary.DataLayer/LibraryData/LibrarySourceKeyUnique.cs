using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Interface for the Library Source Unique Key
    /// </summary>
    public interface ILibrarySourceKeyUnique : IKey
    {
        /// <summary>
        /// Assembly Name for the Library Source
        /// </summary>
        String? AssemblyName { get; }
    }

    /// <summary>
    /// Implementation of the Library Source Unique Key 
    /// </summary>
    public class LibrarySourceKeyUnique: ILibrarySourceKeyUnique, IKeyComparable<ILibrarySourceKeyUnique>
    {
        /// <inheritdoc/>
        public string AssemblyName { get; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Alias
        /// </summary>
        /// <param name="source"></param>
        public LibrarySourceKeyUnique(ILibrarySourceKeyUnique source) : base()
        { if (source.AssemblyName is string) { AssemblyName = source.AssemblyName; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(ILibrarySourceKeyUnique? other)
        {
            return
                other is ILibrarySourceKeyUnique &&
                !string.IsNullOrEmpty(AssemblyName) &&
                !string.IsNullOrEmpty(other.AssemblyName) &&
                AssemblyName.Equals(other.AssemblyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibrarySourceKeyUnique value && Equals(new LibrarySourceKeyUnique(value)); }

        /// <inheritdoc/>
        public int CompareTo(ILibrarySourceKeyUnique? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(AssemblyName, other.AssemblyName, true); }
        }

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        { if (obj is ILibrarySourceKeyUnique value) { return CompareTo(new LibrarySourceKeyUnique(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(LibrarySourceKeyUnique left, LibrarySourceKeyUnique right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibrarySourceKeyUnique left, LibrarySourceKeyUnique right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(LibrarySourceKeyUnique left, LibrarySourceKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(LibrarySourceKeyUnique left, LibrarySourceKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(LibrarySourceKeyUnique left, LibrarySourceKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(LibrarySourceKeyUnique left, LibrarySourceKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AssemblyName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AssemblyName is string)
            { return AssemblyName; }
            else { return string.Empty; }
        }

    }
}
