using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Domain Reference Key
    /// </summary>
    public interface IDomainKeyReference : IKey
    {
        /// <summary>
        /// Database Catalog Name for the Domain.
        /// </summary>
        String? DomainCatalog { get; }

        /// <summary>
        /// Database Schema Name for the Domain
        /// </summary>
        String? DomainSchema { get; }

        /// <summary>
        /// Database Domain Name.
        /// </summary>
        String? DomainName { get; }
    }

    /// <summary>
    /// Implementation of the Database Domain Reference Key
    /// </summary>
    public class DomainKeyReference : IDomainKeyReference, IEquatable<IDomainKeyReference>
    {
        /// <inheritdoc/>
        public String DomainCatalog { get; init; }

        /// <inheritdoc/>
        public String DomainSchema { get; init; }

        /// <inheritdoc/>
        public String DomainName { get; init; }

        /// <summary>
        /// Constructor for the Database Domain Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DomainKeyReference(IDomainKeyReference source) : base()
        {
            if (source.DomainCatalog is string) { DomainCatalog = source.DomainCatalog; }
            else { DomainCatalog = string.Empty; }

            if (source.DomainSchema is string) { DomainSchema = source.DomainSchema; }
            else { DomainSchema = string.Empty; }

            if (source.DomainName is string) { DomainName = source.DomainName; }
            else { DomainName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainKeyReference? other)
        {
            return
                other is IDomainKeyReference &&
                !string.IsNullOrEmpty(DomainCatalog) &&
                !string.IsNullOrEmpty(other.DomainCatalog) &&
                !string.IsNullOrEmpty(DomainSchema) &&
                !string.IsNullOrEmpty(other.DomainSchema) &&
                !string.IsNullOrEmpty(DomainName) &&
                !string.IsNullOrEmpty(other.DomainName) &&
                DomainCatalog.Equals(other.DomainCatalog, KeyExtension.CompareString) &&
                DomainSchema.Equals(other.DomainSchema, KeyExtension.CompareString) &&
                DomainName.Equals(other.DomainName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainKeyReference value && Equals(new DomainKeyReference(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDomainKeyReference? other)
        {
            if (other is null) { return 1; }
            else
            {
                if (string.Compare(DomainCatalog,
                                   other.DomainCatalog,
                                   true) is int catalogValue && catalogValue != 0)
                { return catalogValue; }
                else if (string.Compare(DomainSchema,
                                   other.DomainSchema,
                                   true) is int schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return string.Compare(DomainName, other.DomainName, true); }
            }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainKeyReference value) { return CompareTo(new DomainKeyReference(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainKeyReference left, DomainKeyReference right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainKeyReference left, DomainKeyReference right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainKeyReference left, DomainKeyReference right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainKeyReference left, DomainKeyReference right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainKeyReference left, DomainKeyReference right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainKeyReference left, DomainKeyReference right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), DomainName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (DomainCatalog is string && DomainSchema is string && DomainName is string)
            { return string.Format("{0}.{1}.{2}", DomainCatalog, DomainSchema, DomainName); }
            else { return string.Empty; }
        }


    }
}
