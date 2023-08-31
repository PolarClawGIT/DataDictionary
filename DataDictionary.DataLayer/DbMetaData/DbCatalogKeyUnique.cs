namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbCatalogKeyUnique
    {
        String? CatalogName { get; }
    }

    public class DbCatalogKeyUnique : IDbCatalogKeyUnique, IEquatable<DbCatalogKeyUnique>, IComparable<DbCatalogKeyUnique>, IComparable
    { // I would have used Record instead of Class but Record limits what I can do with IEquatable, IComparable, and ToString

        public String CatalogName { get; init; } = String.Empty;

        public DbCatalogKeyUnique(IDbCatalogKeyUnique source) : base()
        {
            if (source.CatalogName is String) { CatalogName = source.CatalogName; }
            else { CatalogName = String.Empty; }
        }

        #region IEquatable, IComparable
        public virtual bool Equals(DbCatalogKeyUnique? other)
        {
            return (
                other is DbCatalogKeyUnique &&
                !String.IsNullOrEmpty(CatalogName) &&
                !String.IsNullOrEmpty(other.CatalogName) &&
                CatalogName.Equals(other.CatalogName, ModelFactory.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDbCatalogKeyUnique value && this.Equals(new DbCatalogKeyUnique(value));  } 

        public virtual int CompareTo(DbCatalogKeyUnique? other)
        {
            if (other is DbCatalogKeyUnique value)
            { return String.Compare(CatalogName, value.CatalogName, true); }
            else { return 1; }
        }

        public virtual int CompareTo(object? obj)
        { if (obj is IDbCatalogKeyUnique value) { return this.CompareTo(new DbCatalogKeyUnique(value)); } else { return 1; } }

        public static bool operator ==(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return left.Equals(right); }

        public static bool operator !=(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return !left.Equals(right); }

        public static bool operator <(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName); }
        #endregion

        public override String ToString()
        {
            if (CatalogName is String) { return CatalogName; }
            else { return String.Empty; }
        }
    }
}
