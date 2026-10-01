using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Composite Key of the Schema and Document Name.
    /// </summary>
    public interface ISchemaDocumentKeyName : ISchemaDefinitionKey, IDocumentNameKey
    { }

    /// <summary>
    /// Composite Key of the Schema and Document Name.
    /// </summary>
    public class SchemaDocumentKeyName : ISchemaDocumentKeyName,
        IKeyEquality<SchemaDocumentKeyName>,
        IKeyEquality<ISchemaDocumentKeyName>
    {
        /// <inheritdoc/>
        public String DataFileName { get; init; } = string.Empty;

        /// <inheritdoc/>
        public Guid? SchemaId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return SchemaId.HasValue && SchemaId != Guid.Empty && !String.IsNullOrEmpty(DataFileName); } }

        /// <summary>
        /// Constructor for Composite Schema Document Name
        /// </summary>
        protected SchemaDocumentKeyName(): base() { }

        /// <summary>
        /// Constructor for Composite Schema Document Name
        /// </summary>
        /// <param name="source"></param>
        public SchemaDocumentKeyName(ISchemaDocumentKeyName source) : base()
        {
            if (source.SchemaId is Guid) { SchemaId = source.SchemaId; }
            else { SchemaId = Guid.Empty; }

            if (source.DataFileName is String) { DataFileName = source.DataFileName; }
            else { DataFileName = String.Empty; }
        }

        /// <summary>
        /// Constructor for Composite Schema Document Name
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public SchemaDocumentKeyName(ISchemaDefinitionKey schema, IDocumentNameKey document) : base()
        {
            if (schema.SchemaId is Guid) { SchemaId = schema.SchemaId; }
            else { SchemaId = Guid.Empty; }

            if (document.DataFileName is String) { DataFileName = document.DataFileName; }
            else { DataFileName = String.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SchemaDocumentKeyName? other)
        {
            return other is SchemaDocumentKeyName key
                && SchemaId.HasValue && SchemaId != Guid.Empty
                && key.SchemaId.HasValue && key.SchemaId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(SchemaId, other.SchemaId)
                && !String.IsNullOrEmpty(DataFileName)
                && !String.IsNullOrEmpty(other.DataFileName)
                && DataFileName.Equals(other.DataFileName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaDocumentKeyName? other)
        { return other is ISchemaDocumentKeyName key && Equals(new SchemaDocumentKeyName(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISchemaDocumentKeyName key && Equals(new SchemaDocumentKeyName(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SchemaDocumentKeyName left, SchemaDocumentKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SchemaDocumentKeyName left, SchemaDocumentKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(SchemaId, DataFileName); }


        #endregion

    }
}
