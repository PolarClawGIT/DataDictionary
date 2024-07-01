using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for Scripting ScriptAs Key.
    /// </summary>
    public interface IDirectoryTypeKey : IKey
    {
        /// <summary>
        /// Root Directory to place documents in (must be a supported Special Folder).
        /// </summary>
        TemplateDirectoryType RootDirectory { get; }
    }

    /// <summary>
    /// Implementation of the Directory Type Key.
    /// </summary>
    public class DirectoryTypeKey : IDirectoryTypeKey, IKeyComparable<IDirectoryTypeKey>, IParsable<DirectoryTypeKey>
    {
        /// <inheritdoc/>
        public TemplateDirectoryType RootDirectory { get; init; } = TemplateDirectoryType.Null;

        /// <summary>
        /// Basic Constructor for the Element Data As Key.
        /// </summary>
        protected DirectoryTypeKey() : base() { }

        /// <summary>
        /// Constructor for the Element Data As Key.
        /// </summary>
        /// <param name="source"></param>
        public DirectoryTypeKey(TemplateDirectoryType source) : this()
        { RootDirectory = source; }

        /// <summary>
        /// Constructor for the  Element Data As Key.
        /// </summary>
        /// <param name="source"></param>
        public DirectoryTypeKey(IDirectoryTypeKey source) : this()
        { if (source is IDirectoryTypeKey) { RootDirectory = source.RootDirectory; } }

        /// <summary>
        /// Converts a DirectoryType into a DirectoryTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DirectoryTypeKey(TemplateDirectoryType source)
        { return new DirectoryTypeKey(source); }

        /// <summary>
        /// Converts a DirectoryTypeKey into a DirectoryType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator TemplateDirectoryType(DirectoryTypeKey source)
        { return source.RootDirectory; }

        /// <summary>
        /// Returns the Physical Directory being referenced.
        /// </summary>
        /// <returns></returns>
        public DirectoryInfo? ToDirectoryInfo()
        {
            if (parseName.ContainsKey(RootDirectory))
            { return parseName[RootDirectory]; }
            else { return null; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(IDirectoryTypeKey? other)
        {
            return other is IDirectoryTypeKey
                && this.RootDirectory != TemplateDirectoryType.Null
                && other.RootDirectory != TemplateDirectoryType.Null
                && this.RootDirectory.Equals(other.RootDirectory);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IDirectoryTypeKey? other)
        {
            if (other is IDirectoryTypeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDirectoryTypeKey value) { return CompareTo(new DirectoryTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDirectoryTypeKey value && this.Equals(new DirectoryTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DirectoryTypeKey left, DirectoryTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DirectoryTypeKey left, DirectoryTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DirectoryTypeKey left, DirectoryTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DirectoryTypeKey left, DirectoryTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DirectoryTypeKey left, DirectoryTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DirectoryTypeKey left, DirectoryTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return RootDirectory.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the DirectoryType Enum to what the Database uses as TableType Name.
        /// </summary>
        static Dictionary<TemplateDirectoryType, DirectoryInfo> parseName = new Dictionary<TemplateDirectoryType, DirectoryInfo>()
        {
            { TemplateDirectoryType.MySources,    new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "source","repos")) },
            { TemplateDirectoryType.MyDocuments,  new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) },
            { TemplateDirectoryType.MyDownloads,  new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")) },

        };

        /// <inheritdoc/>
        public static DirectoryTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }
            
            if (DirectoryTypeKey.TryParse(source, provider, out DirectoryTypeKey? result))
            { return result; }
            else
            { return TemplateDirectoryType.Null; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out DirectoryTypeKey result)
        { return TryParse(source, out result); }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out DirectoryTypeKey result)
        {
            if (parseName.FirstOrDefault(w => String.Equals(w.Value.ToString(), source, KeyExtension.CompareString))
                is KeyValuePair<TemplateDirectoryType, DirectoryInfo> dbItem
                && dbItem.Key != TemplateDirectoryType.Null)
            { result = new DirectoryTypeKey() { RootDirectory = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of DirectoryTypeKey for each DirectoryType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DirectoryTypeKey> Items()
        { return Enum.GetValues(typeof(TemplateDirectoryType)).Cast<TemplateDirectoryType>().Select(s => new DirectoryTypeKey() { RootDirectory = s }); }

        /// <summary>
        /// Returns the TableType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(RootDirectory)) { return parseName[RootDirectory].ToString(); }
            else { return RootDirectory.ToString(); }
        }
    }
}
