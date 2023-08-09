using DataDictionary.DataLayer.DomainData;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbCatalogKey
    {
        Guid? CatalogId { get; }
    }

    public class DbCatalogKey : IEquatable<DbCatalogKey>
    {
        public Guid CatalogId { get; init; } = Guid.Empty;

        public DbCatalogKey(IDbCatalogKey source) : base()
        {
            if (source.CatalogId is Guid value) { CatalogId = value; }
            else { CatalogId = Guid.Empty; }
        }

        #region IEquatable
        public virtual bool Equals(DbCatalogKey? other)
        {
            return other is DbCatalogKey key &&
                EqualityComparer<Guid?>.Default.Equals(CatalogId, key.CatalogId);
        }

        public override bool Equals(object? other)
        { 
            return other is IDbCatalogKey key &&
                EqualityComparer<Guid?>.Default.Equals(CatalogId, key.CatalogId);
        }

        public static bool operator ==(DbCatalogKey left, DbCatalogKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbCatalogKey left, DbCatalogKey right)
        { return !left.Equals(right); }


        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogId); }
        #endregion

        public override String ToString()
        { return CatalogId.ToString(); }
    }
}
