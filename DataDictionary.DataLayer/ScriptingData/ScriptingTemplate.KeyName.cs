using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData
{
    /// <summary>
    /// Interface for the Scripting Template Name
    /// </summary>
    public interface IScriptingTemplateKeyName : IKey
    {
        /// <summary>
        /// Title of the Template.
        /// </summary>
        String? TemplateTitle { get; }
    }

    /// <summary>
    /// Implementation for Scripting Template Name
    /// </summary>
    public class ScriptingTemplateKeyName : IScriptingTemplateKeyName,
        IKeyComparable<IScriptingTemplateKeyName>, IKeyComparable<ScriptingTemplateKeyName>
    {
        /// <inheritdoc/>
        public String TemplateTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Scripting Template Name
        /// </summary>
        protected internal ScriptingTemplateKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Template Name.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingTemplateKeyName(IScriptingTemplateKeyName source) : base()
        {
            if (source.TemplateTitle is string) { TemplateTitle = source.TemplateTitle; }
            else { TemplateTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ScriptingTemplateKeyName? other)
        {
            return
                other is ScriptingTemplateKeyName &&
                !string.IsNullOrEmpty(TemplateTitle) &&
                !string.IsNullOrEmpty(other.TemplateTitle) &&
                TemplateTitle.Equals(other.TemplateTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IScriptingTemplateKeyName? other)
        { return other is IScriptingTemplateKeyName value && Equals(new ScriptingTemplateKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IScriptingTemplateKeyName value && Equals(new ScriptingTemplateKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ScriptingTemplateKeyName? other)
        {
            if (other is ScriptingTemplateKeyName value)
            { return string.Compare(TemplateTitle, value.TemplateTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IScriptingTemplateKeyName? other)
        { if (other is IScriptingTemplateKeyName value) { return CompareTo(new ScriptingTemplateKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IScriptingTemplateKeyName value) { return CompareTo(new ScriptingTemplateKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptingTemplateKeyName left, ScriptingTemplateKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptingTemplateKeyName left, ScriptingTemplateKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ScriptingTemplateKeyName left, ScriptingTemplateKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ScriptingTemplateKeyName left, ScriptingTemplateKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ScriptingTemplateKeyName left, ScriptingTemplateKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ScriptingTemplateKeyName left, ScriptingTemplateKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return TemplateTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return TemplateTitle; }
    }
}
