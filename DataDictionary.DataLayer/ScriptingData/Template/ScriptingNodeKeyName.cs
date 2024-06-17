using DataDictionary.DataLayer.ApplicationData.Scope;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Unique Key on Property for the Scripting Template Node.
    /// </summary>
    public interface IScriptingNodeKeyName :IKey
    {
        /// <summary>
        /// Scope of the Property (column) of the Node (used to select the item to render).
        /// </summary>
        ScopeType PropertyScope { get; }

        /// <summary>
        ///Name of the Property (column) of the Node (used to select the item to render).
        /// </summary>
        String? PropertyName { get; }
    }

    /// <summary>
    /// Implementation for the Unique Key on Property for the Scripting Template Node.
    /// </summary>
    public class ScriptingNodeKeyName : IScriptingNodeKeyName, IKeyComparable<IScriptingNodeKeyName>
    {
        /// <inheritdoc/>
        public ScopeType PropertyScope { get; protected set; } = ScopeType.Null;

        /// <inheritdoc/>
        public String PropertyName { get; protected set; } = String.Empty;

        /// <summary>
        /// Constructor for the Unique Key on Property for the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingNodeKeyName(IScriptingNodeKeyName source) : base()
        {
            PropertyScope = source.PropertyScope;

            if (source.PropertyName is String) { PropertyName = source.PropertyName; }
            else { PropertyName = String.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IScriptingNodeKeyName? other)
        {
            return
                other is IScriptingNodeKeyName &&
                PropertyScope.Equals(other.PropertyScope) &&
                !string.IsNullOrEmpty(PropertyName) &&
                !string.IsNullOrEmpty(other.PropertyName) &&
                PropertyName.Equals(other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptingNodeKeyName value && Equals(new ScriptingNodeKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IScriptingNodeKeyName? other)
        {
            if (other is null) { return 1; }
            else if (PropertyScope.CompareTo(other.PropertyScope) is int value && value != 0) { return value; }
            else { return String.Compare(PropertyName, other.PropertyName, true);  }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScriptingNodeKeyName value) { return CompareTo(new ScriptingNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(PropertyScope, PropertyName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return PropertyName; }

    }
}
