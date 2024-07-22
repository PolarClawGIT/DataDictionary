using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Scripting Template Path Key
    /// </summary>
    public interface IScriptingPathKeyName : IKey
    {
        /// <summary>
        /// Name of the Scripting Template Path.
        /// </summary>
        String? PathName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Path Key
    /// </summary>
    public class ScriptingPathKeyName : IScriptingPathKeyName, IKeyComparable<IScriptingPathKeyName>
    {
        /// <inheritdoc/>
        public String PathName { get; init; } = string.Empty;

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
            if (source.PathName is string) { PathName = source.PathName; }
            else { PathName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IScriptingPathKeyName? other)
        {
            return
                other is IScriptingPathKeyName &&
                !string.IsNullOrEmpty(PathName) &&
                !string.IsNullOrEmpty(other.PathName) &&
                PathName.Equals(other.PathName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptingPathKeyName value && Equals(new ScriptingPathKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IScriptingPathKeyName? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(PathName, other.PathName, true); }
        }

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
        { return PathName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (PathName is string) { return PathName; }
            else { return string.Empty; }
        }
    }
}
