﻿using DataDictionary.DataLayer.DomainData;

namespace DataDictionary.DataLayer.DatabaseData
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
        { return other is DbCatalogKey && EqualityComparer<Guid?>.Default.Equals(this.CatalogId, other.CatalogId); }

        public override bool Equals(object? other)
        { return other is IDbCatalogKey value && this.Equals(new DbCatalogKey(value)); }

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