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

        #region IEquatable, IComparable
        public virtual bool Equals(DbCatalogKey? other)
        {
            return (
                other is DbCatalogKey &&
                !Guid.Empty.Equals(CatalogId) &&
                !Guid.Empty.Equals(other.CatalogId) &&
                CatalogId.Equals(other.CatalogId));
        }

        public virtual int CompareTo(object? obj)
        { if (obj is DbCatalogKey value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is DbCatalogKey value) { return this.Equals(value); } else { return false; } }

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
