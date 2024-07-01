using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Member
{

    /// <summary>
    /// Interface for Database MemberType Key.
    /// </summary>
    public interface ILibraryMemberTypeKey : IKey
    {
        /// <summary>
        /// Type of Member (NameSpace, Type, Property, Field, ...)
        /// </summary>
        LibraryMemberType MemberType { get; }
    }

    /// <summary>
    /// Implementation for Database MemberType Key.
    /// </summary>
    public class LibraryMemberTypeKey : ILibraryMemberTypeKey, IKeyComparable<ILibraryMemberTypeKey>, IParsable<LibraryMemberTypeKey>
    {
        /// <inheritdoc/>
        public LibraryMemberType MemberType { get; init; } = LibraryMemberType.Null;

        /// <summary>
        /// Code use by XML Document for the Type
        /// </summary>
        public Char Code
        {
            get
            {
                if (parseName.ContainsKey(MemberType)) { return parseName[MemberType].Code; }
                else { return ' '; }
            }
        }

        /// <summary>
        /// Name assigned to the Type for Db Storage.
        /// </summary>
        public String Name
        {
            get
            {
                if (parseName.ContainsKey(MemberType)) { return parseName[MemberType].Name; }
                else { return String.Empty; }
            }
        }

        /// <summary>
        /// Basic Constructor for the MemberType Key.
        /// </summary>
        protected LibraryMemberTypeKey() : base() { }

        /// <summary>
        /// Constructor for the MemberType Key.
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberTypeKey(LibraryMemberType source) : this()
        { MemberType = source; }

        /// <summary>
        /// Constructor for the MemberType Key.
        /// </summary>
        /// <param name="source"></param>
        public LibraryMemberTypeKey(ILibraryMemberTypeKey source) : this()
        { if (source is ILibraryMemberTypeKey) { MemberType = source.MemberType; } }

        /// <summary>
        /// Converts a LibraryMemberType into a LibraryMemberTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator LibraryMemberTypeKey(LibraryMemberType source) { return new LibraryMemberTypeKey(source); }

        /// <summary>
        /// Converts a LibraryMemberTypeKey into a LibraryMemberType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator LibraryMemberType(LibraryMemberTypeKey source) { return source.MemberType; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(ILibraryMemberTypeKey? other)
        {
            return other is ILibraryMemberTypeKey
                && this.MemberType != LibraryMemberType.Null
                && other.MemberType != LibraryMemberType.Null
                && this.MemberType.Equals(other.MemberType);
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ILibraryMemberTypeKey? other)
        {
            if (other is ILibraryMemberTypeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ILibraryMemberTypeKey value) { return CompareTo(new LibraryMemberTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ILibraryMemberTypeKey value && this.Equals(new LibraryMemberTypeKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(LibraryMemberTypeKey left, LibraryMemberTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(LibraryMemberTypeKey left, LibraryMemberTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(LibraryMemberTypeKey left, LibraryMemberTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(LibraryMemberTypeKey left, LibraryMemberTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(LibraryMemberTypeKey left, LibraryMemberTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(LibraryMemberTypeKey left, LibraryMemberTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return MemberType.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the LibraryMemberType Enum to what the Database uses as MemberType Name.
        /// </summary>
        static Dictionary<LibraryMemberType, (Char Code, String Name)> parseName = new Dictionary<LibraryMemberType, (Char Code, String Name)>()
        {
            { LibraryMemberType.NameSpace,  new ('N',"NameSpace")},
            { LibraryMemberType.Type,       new ('T',"Type")},
            { LibraryMemberType.Field,      new ('F',"Field")},
            { LibraryMemberType.Property,   new ('P',"Property")},
            { LibraryMemberType.Method,     new ('M',"Method")},
            { LibraryMemberType.Event,      new ('E',"Event")},
            { LibraryMemberType.Parameter,  new ('@',"Parameter")},
        };

        /// <inheritdoc/>
        public static LibraryMemberTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (LibraryMemberTypeKey.TryParse(source, out LibraryMemberTypeKey? result))
            { return result; }
            else
            { return LibraryMemberType.Null; }
        }

        /// <inheritdoc/>
        public static Boolean TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out LibraryMemberTypeKey result)
        { return TryParse(source, out result); }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static Boolean TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out LibraryMemberTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Name.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<LibraryMemberType, (Char Code, String Name)> itemName
                && itemName.Key != LibraryMemberType.Null)
            { result = new LibraryMemberTypeKey(itemName.Key); return true; }

            else if (parseName.FirstOrDefault(w => source is String && source.Length > 0 && w.Value.Code.Equals(source[0]))
                is KeyValuePair<LibraryMemberType, (Char Code, String Name)> itemCode
                && itemCode.Key != LibraryMemberType.Null)
            { result = new LibraryMemberTypeKey(itemCode.Key); return true; }

            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of LibraryMemberTypeKey for each LibraryMemberType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<LibraryMemberTypeKey> Items()
        { return Enum.GetValues(typeof(LibraryMemberType)).Cast<LibraryMemberType>().Select(s => new LibraryMemberTypeKey() { MemberType = s }); }

        /// <summary>
        /// Returns the MemberType Name.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            if (parseName.ContainsKey(MemberType)) { return parseName[MemberType].Name; }
            else { return MemberType.ToString(); }
        }
    }


    /// <summary>
    /// Support for LibraryMemberType
    /// </summary>
    public static class LibraryMemberTypeExtension
    {
        /// <summary>
        /// Returns the Library Member Type Name
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/"/>
        public static String? ToName(this LibraryMemberType source)
        { return new LibraryMemberTypeKey(source).Name; }

        /// <summary>
        /// Returns the Library Member Type Code.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/"/>
        public static Char? ToCode(this LibraryMemberType source)
        { return new LibraryMemberTypeKey(source).Code; }
    }
}
