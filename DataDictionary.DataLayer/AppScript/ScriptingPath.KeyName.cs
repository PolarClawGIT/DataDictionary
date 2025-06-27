using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template Path Key
    /// </summary>
    public interface IScriptingPathKeyName : IKey
    {
        /// <summary>
        /// Name of the Scripting Template Path.
        /// </summary>
        String? NameSpace { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Path Key
    /// </summary>
    public class ScriptingPathKeyName : IScriptingPathKeyName,
        IKeyComparable<IScriptingPathKeyName>, IKeyComparable<ScriptingPathKeyName>
    {
        /// <inheritdoc/>
        public String NameSpace { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Scripting Template Path Key
        /// </summary>
        protected ScriptingPathKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Template Path Key
        /// </summary>
        /// <param name="source"></param>
        public ScriptingPathKeyName(IScriptingPathKeyName source) : this()
        {
            if (source.NameSpace is string) { NameSpace = source.NameSpace; }
            else { NameSpace = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ScriptingPathKeyName? other)
        {
            return
                other is ScriptingPathKeyName &&
                !string.IsNullOrEmpty(NameSpace) &&
                !string.IsNullOrEmpty(other.NameSpace) &&
                NameSpace.Equals(other.NameSpace, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual bool Equals(IScriptingPathKeyName? other)
        { return other is IScriptingPathKeyName value && Equals(new ScriptingPathKeyName(value)); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptingPathKeyName value && Equals(new ScriptingPathKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ScriptingPathKeyName? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(NameSpace, other.NameSpace, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IScriptingPathKeyName? other)
        { if (other is IScriptingPathKeyName value) { return CompareTo(new ScriptingPathKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScriptingPathKeyName value) { return CompareTo(new ScriptingPathKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ScriptingPathKeyName left, ScriptingPathKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScriptingPathKeyName left, ScriptingPathKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScriptingPathKeyName left, ScriptingPathKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScriptingPathKeyName left, ScriptingPathKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScriptingPathKeyName left, ScriptingPathKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScriptingPathKeyName left, ScriptingPathKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return NameSpace.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (NameSpace is string) { return NameSpace; }
            else { return string.Empty; }
        }
    }
}
