using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{

    /// <summary>
    /// Interface for the Scripting Object Node
    /// </summary>
    public interface ISchemaNodeObjectName: IKey
    {
        /// <summary>
        /// Object Scope of the item to be rendered.
        /// </summary>
        ScopeType ObjectScope { get; }

        /// <summary>
        /// The Property within the Object to render. Null = object itself.
        /// </summary>
        String? ObjectProperty { get; }
    }


    /// <summary>
    /// Interface for the Scripting Object Node Key Name
    /// </summary>
    public interface ISchemaNodeKeyName : ISchemaNodeObjectName, ISchemaDefinitionKey
    { }

    /// <summary>
    /// Implementation of the Schema Object Node Key Name
    /// </summary>
    public class SchemaNodeKeyName : SchemaDefinitionKey, ISchemaNodeKeyName,
        IKeyComparable<ISchemaNodeKeyName>, IKeyComparable<SchemaNodeKeyName>
    {
        /// <inheritdoc/>
        public ScopeType ObjectScope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public String? ObjectProperty { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Schema Node Key Name.
        /// </summary>
        protected SchemaNodeKeyName() : base() { }

        /// <summary>
        /// Constructor for the Schema Node Key Name.
        /// </summary>
        /// <param name="source"></param>
        public SchemaNodeKeyName(ISchemaNodeKeyName source) : this()
        {
            ObjectScope = source.ObjectScope;
            ObjectProperty = source.ObjectProperty;
        }

        /// <inheritdoc/>
        public override Boolean HasValue { get { return base.HasValue && !ObjectScope.Equals(ScopeType.Null); } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeKeyName? other)
        {
            return
                other is SchemaNodeKeyName &&
                new SchemaDefinitionKey(this).Equals(other) &&
                !ObjectScope.Equals(ScopeType.Null) &&
                !other.ObjectScope.Equals(ScopeType.Null) &&
                ObjectScope.Equals(other.ObjectScope);
        }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeKeyName? other)
        { return other is ISchemaNodeKeyName value && Equals(new SchemaNodeKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(SchemaNodeKeyName? other)
        {
            if (other is null ) { return 1; }
            else if(!HasValue) { return -1; }
            else if(!other.HasValue) { return 1; }
            else if(ObjectScope.CompareTo(other.ObjectScope) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ObjectProperty??String.Empty, other.ObjectProperty??String.Empty, true); }
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ISchemaNodeKeyName value && Equals(new SchemaNodeKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ISchemaNodeKeyName? other)
        { if (other is ISchemaNodeKeyName value) { return CompareTo(new SchemaNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public Int32 CompareTo(object? obj)
        { if (obj is ISchemaNodeKeyName value) { return CompareTo(new SchemaNodeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaNodeKeyName left, SchemaNodeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaNodeKeyName left, SchemaNodeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SchemaNodeKeyName left, SchemaNodeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(SchemaNodeKeyName left, SchemaNodeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(SchemaNodeKeyName left, SchemaNodeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SchemaNodeKeyName left, SchemaNodeKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectScope.GetHashCode(), (ObjectProperty ?? String.Empty).GetHashCode(KeyExtension.CompareString)); }
        #endregion
        /// <inheritdoc/>
        public override String ToString()
        {
            String result = ObjectScope.GetName();
            if (!String.IsNullOrWhiteSpace(ObjectProperty))
            { result = String.Concat(result, ".", ObjectProperty); }

            return result;
        }


    }
}
