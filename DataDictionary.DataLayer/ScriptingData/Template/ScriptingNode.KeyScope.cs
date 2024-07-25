using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Key on Property Scope for the Scripting Template Node.
    /// </summary>
    public interface IScriptingNodeKeyScope : IKey
    {
        /// <summary>
        /// Scope of the Property (column) of the Node (used to select the item to render).
        /// </summary>
        ScopeType PropertyScope { get; }
    }

    /// <summary>
    /// Implementation for the Key on Property Scope for the Scripting Template Node.
    /// </summary>
    public class ScriptingNodeKeyScope : IScriptingNodeKeyScope,
        IKeyComparable<IScriptingNodeKeyScope>, IKeyComparable<ScriptingNodeKeyScope>
    {
        /// <inheritdoc/>
        public ScopeType PropertyScope { get; protected set; } = ScopeType.Null;

        /// <summary>
        /// Constructor for the Unique Key on Property for the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingNodeKeyScope(IScriptingNodeKeyScope source) : base()
        {
            PropertyScope = source.PropertyScope;
        }

        /// <summary>
        /// Cast from ScriptingNodeKeyName to ScriptingNodeKeyScope
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ScriptingNodeKeyScope(ScriptingNodeKeyName source) 
        { return new ScriptingNodeKeyScope(source); }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ScriptingNodeKeyScope? other)
        {
            return
                other is IScriptingNodeKeyScope &&
                PropertyScope.Equals(other.PropertyScope);
        }

        /// <inheritdoc/>
        public virtual bool Equals(IScriptingNodeKeyScope? other)
        { return other is IScriptingNodeKeyScope value && Equals(new ScriptingNodeKeyScope(value)); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptingNodeKeyScope value && Equals(new ScriptingNodeKeyScope(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ScriptingNodeKeyScope? other)
        {
            if (other is null) { return 1; }
            else { return PropertyScope.CompareTo(other.PropertyScope); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IScriptingNodeKeyScope? other)
        { if (other is IScriptingNodeKeyScope value) { return CompareTo(new ScriptingNodeKeyScope(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IScriptingNodeKeyScope value) { return CompareTo(new ScriptingNodeKeyScope(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ScriptingNodeKeyScope left, ScriptingNodeKeyScope right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScriptingNodeKeyScope left, ScriptingNodeKeyScope right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ScriptingNodeKeyScope left, ScriptingNodeKeyScope right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScriptingNodeKeyScope left, ScriptingNodeKeyScope right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ScriptingNodeKeyScope left, ScriptingNodeKeyScope right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ScriptingNodeKeyScope left, ScriptingNodeKeyScope right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return PropertyScope.GetHashCode(); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return ScopeEnumeration.Cast(PropertyScope).Name; }
    }
}
