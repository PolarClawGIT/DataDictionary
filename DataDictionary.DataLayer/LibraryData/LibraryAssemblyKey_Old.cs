using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Primary Key for the LibraryAssembly.
    /// </summary>
    [Obsolete]
    public interface ILibraryAssemblyKey_old
    {
        /// <summary>
        /// Assembly Id, GUID assigned by the application. This is the Primary Key.
        /// </summary>
        Nullable<Guid> AssemblyId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key for LibraryAssembly.
    /// </summary>
    [Obsolete]
    public class LibraryAssemblyKey_Old : ILibraryAssemblyKey_old, IEquatable<LibraryAssemblyKey_Old>
    {

        /// <inheritdoc/>
        public Nullable<Guid> AssemblyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for LibraryAssemblyKey
        /// </summary>
        /// <param name="source"></param>
        public LibraryAssemblyKey_Old(ILibraryAssemblyKey_old source) : base()
        {
            if (source.AssemblyId is Guid) { AssemblyId = source.AssemblyId; }
            else { AssemblyId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(LibraryAssemblyKey_Old? other)
        { return other is ILibraryAssemblyKey_old key && EqualityComparer<Guid?>.Default.Equals(AssemblyId, key.AssemblyId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ILibraryAssemblyKey_old value && this.Equals(new LibraryAssemblyKey_Old(value)); }

        /// <inheritdoc/>
        public static bool operator ==(LibraryAssemblyKey_Old left, LibraryAssemblyKey_Old right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(LibraryAssemblyKey_Old left, LibraryAssemblyKey_Old right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(AssemblyId); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AssemblyId is Guid && AssemblyId.ToString() is String value) { return value; }
            else { return String.Empty; }
        }
    }
}
