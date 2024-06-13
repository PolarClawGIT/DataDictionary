using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Implementation for Script As Key.
    /// </summary>
    public class ScriptAsTypeKey : IScriptAsType, IKeyComparable<IScriptAsType>, IParsable<ScriptAsTypeKey>
    {
        /// <inheritdoc/>
        public ScriptAsType ScriptAs { get; init; } = ScriptAsType.none;

        /// <summary>
        /// Basic Constructor for the Script As Key.
        /// </summary>
        protected ScriptAsTypeKey() : base() { }

        /// <summary>
        /// Constructor for the Script As Key.
        /// </summary>
        /// <param name="source"></param>
        public ScriptAsTypeKey(ScriptAsType source) : this()
        { ScriptAs = source; }

        /// <summary>
        /// Constructor for the Script As Key.
        /// </summary>
        /// <param name="source"></param>
        public ScriptAsTypeKey(IScriptAsType source) : this()
        { if (source is IScriptAsType) { ScriptAs = source.ScriptAs; } }

        /// <summary>
        /// Converts a ScriptAsType into a ScriptAsTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ScriptAsTypeKey(ScriptAsType source) { return new ScriptAsTypeKey(source); }

        /// <summary>
        /// Converts a ScriptAsTypeKey into a ScriptAsType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ScriptAsType(ScriptAsTypeKey source) { return source.ScriptAs; }

        /// <summary>
        /// Returns the extension used by the Script type.
        /// </summary>
        /// <returns></returns>
        public String? ToExtension()
        {
            if (parseName.ContainsKey(ScriptAs))
            { return parseName[ScriptAs].Extension; }
            else { return null; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(IScriptAsType? other)
        {
            return other is IScriptAsType
                && this.ScriptAs != ScriptAsType.none
                && other.ScriptAs != ScriptAsType.none
                && this.ScriptAs.Equals(other.ScriptAs);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IScriptAsType? other)
        {
            if (other is IScriptAsType value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScriptAsType value) { return CompareTo(new ScriptAsTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptAsType value && this.Equals(new ScriptAsTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ScriptAsTypeKey left, ScriptAsTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScriptAsTypeKey left, ScriptAsTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScriptAsTypeKey left, ScriptAsTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScriptAsTypeKey left, ScriptAsTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScriptAsTypeKey left, ScriptAsTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScriptAsTypeKey left, ScriptAsTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return ScriptAs.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the ScriptAsType Enum to what the Database uses as TableType Name.
        /// </summary>
        static Dictionary<ScriptAsType, (String Display, String Extension)> parseName = new Dictionary<ScriptAsType, (String Display, String Extension)>()
        {
            { ScriptAsType.CSharp, ("C#",     "cs")},
            { ScriptAsType.VBNet,  ("VB.Net", "vb")},
            { ScriptAsType.MsSql,  ("Ms SQL", "sql")},
            { ScriptAsType.Text,   ("Text",   "txt")},
            { ScriptAsType.XML,    ("XML",    "xml")},
        };

        /// <inheritdoc/>
        public static ScriptAsTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (ScriptAsTypeKey.TryParse(source, provider, out ScriptAsTypeKey? result))
            { return result; }
            else
            { return ScriptAsType.none; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out ScriptAsTypeKey result)
        { return TryParse(source, out result); }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out ScriptAsTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Display.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<ScriptAsType, (String Display, String Extension)> dbItem
                && dbItem.Key != ScriptAsType.none)
            { result = new ScriptAsTypeKey() { ScriptAs = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of ScriptAsTypeKey for each ScriptAsType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ScriptAsTypeKey> Items()
        { return Enum.GetValues(typeof(ScriptAsType)).Cast<ScriptAsType>().Select(s => new ScriptAsTypeKey() { ScriptAs = s }); }

        /// <summary>
        /// Returns the TableType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(ScriptAs)) { return parseName[ScriptAs].Display; }
            else { return ScriptAs.ToString(); }
        }
    }
}
