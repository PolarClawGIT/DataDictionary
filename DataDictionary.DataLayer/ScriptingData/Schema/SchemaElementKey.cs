using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Schema Element.
    /// </summary>
    public interface ISchemaElementKey : IKey
    {
        /// <summary>
        /// Schema Id of the Scripting Schema.
        /// </summary>
        Guid? ElementId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Schema Element.
    /// </summary>
    public class SchemaElementKey : ISchemaElementKey, IKeyEquality<ISchemaElementKey>
    {
        /// <inheritdoc/>
        public Guid? ElementId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public SchemaElementKey(ISchemaElementKey source) : base()
        {
            if (source.ElementId is Guid) { ElementId = source.ElementId; }
            else { ElementId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ISchemaElementKey? other)
        { return other is ISchemaElementKey && EqualityComparer<Guid?>.Default.Equals(this.ElementId, other.ElementId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISchemaElementKey value && this.Equals(new SchemaElementKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(SchemaElementKey left, SchemaElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SchemaElementKey left, SchemaElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (ElementId is Guid) { return (ElementId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
