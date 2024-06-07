using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema Name
    /// </summary>
    [Obsolete("To be removed", true)]
    public interface ISchemaKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting Schema.
        /// </summary>
        String? SchemaTitle { get; }
    }

    /// <summary>
    /// Implementation for Scripting Schema Name
    /// </summary>
    [Obsolete("To be removed", true)]
    public class SchemaKeyName : ISchemaKeyName, IKeyComparable<ISchemaKeyName>
    {
        /// <inheritdoc/>
        public String SchemaTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Scripting Schema Name
        /// </summary>
        protected internal SchemaKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Schema Name
        /// </summary>
        /// <param name="source"></param>
        public SchemaKeyName(ISchemaKeyName source) : base()
        {
            if (source.SchemaTitle is string) { SchemaTitle = source.SchemaTitle; }
            else { SchemaTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(ISchemaKeyName? other)
        {
            return
                other is ISchemaKeyName &&
                !string.IsNullOrEmpty(SchemaTitle) &&
                !string.IsNullOrEmpty(other.SchemaTitle) &&
                SchemaTitle.Equals(other.SchemaTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISchemaKeyName value && Equals(new SchemaKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(ISchemaKeyName? other)
        {
            if (other is SchemaKeyName value)
            { return string.Compare(SchemaTitle, value.SchemaTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is ISchemaKeyName value) { return CompareTo(new SchemaKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(SchemaKeyName left, SchemaKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SchemaKeyName left, SchemaKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(SchemaKeyName left, SchemaKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(SchemaKeyName left, SchemaKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(SchemaKeyName left, SchemaKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(SchemaKeyName left, SchemaKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return SchemaTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return SchemaTitle; }
    }
}
