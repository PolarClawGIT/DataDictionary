using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Implementation for Element Data As Key.
    /// </summary>
    public class ElementDataAsTypeKey : IElementDataAsType, IKeyComparable<IElementDataAsType>, IParsable<ElementDataAsTypeKey>
    {
        /// <inheritdoc/>
        public ElementDataAsType DataAs { get; init; } = ElementDataAsType.Null;

        /// <summary>
        /// Basic Constructor for the Element Data As Key.
        /// </summary>
        protected ElementDataAsTypeKey() : base() { }

        /// <summary>
        /// Constructor for the Element Data As Key.
        /// </summary>
        /// <param name="source"></param>
        public ElementDataAsTypeKey(ElementDataAsType source) : this()
        { DataAs = source; }

        /// <summary>
        /// Constructor for the  Element Data As Key.
        /// </summary>
        /// <param name="source"></param>
        public ElementDataAsTypeKey(IElementDataAsType source) : this()
        { if (source is IElementDataAsType) { DataAs = source.DataAs; } }

        /// <summary>
        /// Converts a ElementDataAsType into a ElementDataAsTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ElementDataAsTypeKey(ElementDataAsType source) { return new ElementDataAsTypeKey(source); }

        /// <summary>
        /// Converts a ElementDataAsTypeKey into a ElementDataAsType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ElementDataAsType(ElementDataAsTypeKey source) { return source.DataAs; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(IElementDataAsType? other)
        {
            return other is IElementDataAsType
                && this.DataAs != ElementDataAsType.Null
                && other.DataAs != ElementDataAsType.Null
                && this.DataAs.Equals(other.DataAs);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IElementDataAsType? other)
        {
            if (other is IElementDataAsType value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IElementDataAsType value) { return CompareTo(new ElementDataAsTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IElementDataAsType value && this.Equals(new ElementDataAsTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ElementDataAsTypeKey left, ElementDataAsTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ElementDataAsTypeKey left, ElementDataAsTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ElementDataAsTypeKey left, ElementDataAsTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ElementDataAsTypeKey left, ElementDataAsTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ElementDataAsTypeKey left, ElementDataAsTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ElementDataAsTypeKey left, ElementDataAsTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return DataAs.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the ElementDataAsType Enum to what the Database uses as TableType Name.
        /// </summary>
        static Dictionary<ElementDataAsType, String> parseName = new Dictionary<ElementDataAsType, String>()
        {
            { ElementDataAsType.Text,   "Text"},
            { ElementDataAsType.XML,    "XML"},
            { ElementDataAsType.CData,  "CData"},
        };

        /// <inheritdoc/>
        public static ElementDataAsTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (ElementDataAsTypeKey.TryParse(source, provider, out ElementDataAsTypeKey? result))
            { return result; }
            else
            { return ElementDataAsType.Null; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out ElementDataAsTypeKey result)
        { return TryParse(source, out result); }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out ElementDataAsTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<ElementDataAsType, String> dbItem
                && dbItem.Key != ElementDataAsType.Null)
            { result = new ElementDataAsTypeKey() { DataAs = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of ElementDataAsTypeKey for each ElementDataAsType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ElementDataAsTypeKey> Items()
        { return Enum.GetValues(typeof(ElementDataAsType)).Cast<ElementDataAsType>().Select(s => new ElementDataAsTypeKey() { DataAs = s }); }

        /// <summary>
        /// Returns the TableType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(DataAs)) { return parseName[DataAs]; }
            else { return DataAs.ToString(); }
        }
    }
}
