using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the unique Name of a Schema Definition.
    /// </summary>
    public interface ISchemaDefinitionKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting Schema (aka Name of the Schema)
        /// </summary>
        String? SchemaTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a SchemaDefinition.
    /// </summary>
    public class SchemaDefinitionKeyName : ISchemaDefinitionKeyName,
        IKeyComparable<ISchemaDefinitionKeyName>, IKeyComparable<SchemaDefinitionKeyName>
    {
        /// <inheritdoc/>
        public String SchemaTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(SchemaTitle); } }

        /// <summary>
        /// Constructor for the SchemaDefinition Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public SchemaDefinitionKeyName(ISchemaDefinitionKeyName source) : base()
        {
            if (source.SchemaTitle is string) { SchemaTitle = source.SchemaTitle; }
            else { SchemaTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SchemaDefinitionKeyName? other)
        {
            return
                other is SchemaDefinitionKeyName &&
                !string.IsNullOrEmpty(SchemaTitle) &&
                !string.IsNullOrEmpty(other.SchemaTitle) &&
                SchemaTitle.Equals(other.SchemaTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISchemaDefinitionKeyName? other)
        { return other is ISchemaDefinitionKeyName value && Equals(new SchemaDefinitionKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISchemaDefinitionKeyName value && Equals(new SchemaDefinitionKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(SchemaDefinitionKeyName? other)
        {
            if (other is SchemaDefinitionKeyName value)
            { return string.Compare(SchemaTitle, value.SchemaTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ISchemaDefinitionKeyName? other)
        { if (other is ISchemaDefinitionKeyName value) { return CompareTo(new SchemaDefinitionKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ISchemaDefinitionKeyName value) { return CompareTo(new SchemaDefinitionKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaDefinitionKeyName left, SchemaDefinitionKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaDefinitionKeyName left, SchemaDefinitionKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SchemaDefinitionKeyName left, SchemaDefinitionKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(SchemaDefinitionKeyName left, SchemaDefinitionKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(SchemaDefinitionKeyName left, SchemaDefinitionKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SchemaDefinitionKeyName left, SchemaDefinitionKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return SchemaTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (SchemaTitle is string) { return SchemaTitle; }
            else { return string.Empty; }
        }
    }
}
