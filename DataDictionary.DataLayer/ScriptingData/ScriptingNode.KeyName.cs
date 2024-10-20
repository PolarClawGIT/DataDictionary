﻿using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.ScriptingData
{
    /// <summary>
    /// Interface for the Unique Key on Property for the Scripting Template Node.
    /// </summary>
    public interface IScriptingNodeKeyName : IScriptingNodeKeyScope
    {
        /// <summary>
        ///Name of the Property (column) of the Node (used to select the item to render).
        /// </summary>
        String? PropertyName { get; }
    }

    /// <summary>
    /// Implementation for the Unique Key on Property for the Scripting Template Node.
    /// </summary>
    public class ScriptingNodeKeyName : IScriptingNodeKeyName,
        IKeyComparable<IScriptingNodeKeyName>, IKeyComparable<ScriptingNodeKeyName>
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
        public Boolean Equals(ScriptingNodeKeyName? other)
        {
            return
                other is IScriptingNodeKeyName &&
                new ScriptingNodeKeyScope(this).Equals(new ScriptingNodeKeyScope(other)) &&
                !String.IsNullOrEmpty(PropertyName) &&
                !String.IsNullOrEmpty(other.PropertyName) &&
                PropertyName.Equals(other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IScriptingNodeKeyName? other)
        { return other is IScriptingNodeKeyName value && Equals(new ScriptingNodeKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IScriptingNodeKeyName value && Equals(new ScriptingNodeKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ScriptingNodeKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new ScriptingNodeKeyScope(this).CompareTo(new ScriptingNodeKeyScope(other)) is int value && value != 0) { return value; }
            else { return String.Compare(PropertyName, other.PropertyName, true); }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IScriptingNodeKeyName? other)
        { if (other is IScriptingNodeKeyName value) { return CompareTo(new ScriptingNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IScriptingNodeKeyName value) { return CompareTo(new ScriptingNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ScriptingNodeKeyName left, ScriptingNodeKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(PropertyScope, PropertyName); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return PropertyName; }
    }
}
