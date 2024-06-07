using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Schema.
    /// </summary>
    [Obsolete("To be removed", true)]
    public interface ISchemaKey : IKey
    {
        /// <summary>
        /// Schema Id of the Scripting Schema.
        /// </summary>
        Guid? SchemaId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Schema.
    /// </summary>
    [Obsolete("To be removed", true)]
    public class SchemaKey : ISchemaKey, IKeyEquality<ISchemaKey>
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public SchemaKey(ISchemaKey source) : base()
        {
            if (source.SchemaId is Guid) { SchemaId = source.SchemaId; }
            else { SchemaId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ISchemaKey? other)
        { return other is ISchemaKey && EqualityComparer<Guid?>.Default.Equals(this.SchemaId, other.SchemaId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISchemaKey value && this.Equals(new SchemaKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(SchemaKey left, SchemaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SchemaKey left, SchemaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (SchemaId is Guid) { return (SchemaId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
