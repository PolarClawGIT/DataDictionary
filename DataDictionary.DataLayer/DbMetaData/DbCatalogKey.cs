namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbCatalogKey
    {
        Guid? CatalogId { get; }
    }

    public class DbCatalogKey: IEquatable<DbCatalogKey>
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
            return (
                other is DbCatalogKey &&
                !Guid.Empty.Equals(CatalogId) &&
                !Guid.Empty.Equals(other.CatalogId) &&
                CatalogId.Equals(other.CatalogId));
        }

        public override bool Equals(object? obj)
        { if (obj is IDbCatalogKey value) { return this.Equals(new DbCatalogKey(value)); } else { return false; } }

        public static bool operator ==(DbCatalogKey left, DbCatalogKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbCatalogKey left, DbCatalogKey right)
        { return !left.Equals(right); }


        public override Int32 GetHashCode()
        {   return CatalogId.GetHashCode(); }
        #endregion

        public override String ToString()
        {   return CatalogId.ToString(); }
    }
}
