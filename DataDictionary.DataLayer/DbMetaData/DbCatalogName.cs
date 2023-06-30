using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbCatalogName
    {
        String? CatalogName { get; }
    }

    public static class IDbCatalogNameExtension
    {
        public static DbCatalogName ToCatalogName(this IDbCatalogName catalogName)
        { return new DbCatalogName(catalogName); }
    }

    public class DbCatalogName : IDbCatalogName, IEquatable<IDbCatalogName>, IComparable<IDbCatalogName>, IComparable
    {
        public String? CatalogName { get; init; }

        public DbCatalogName(IDbCatalogName source) : base()
        { CatalogName = source.CatalogName; }

        #region IEquatable, IComparable
        public virtual bool Equals(IDbCatalogName? other)
        {
            return (
                other is IDbCatalogName &&
                !String.IsNullOrEmpty(CatalogName) &&
                !String.IsNullOrEmpty(other.CatalogName) &&
                CatalogName.Equals(other.CatalogName, StringComparison.CurrentCultureIgnoreCase));
        }

        public virtual int CompareTo(IDbCatalogName? other)
        {   if (other is IDbCatalogName value) { return String.Compare(CatalogName, value.CatalogName, true); } else { return 1; } }

        public virtual int CompareTo(object? obj)
        {   if (obj is IDbCatalogName value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        {   if (obj is IDbCatalogName value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbCatalogName left, IDbCatalogName right)
        { return left.Equals(right); }

        public static bool operator !=(DbCatalogName left, IDbCatalogName right)
        { return !left.Equals(right); }

        public static bool operator <(DbCatalogName left, IDbCatalogName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbCatalogName left, IDbCatalogName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbCatalogName left, IDbCatalogName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbCatalogName left, IDbCatalogName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String) { return (CatalogName).GetHashCode(); }
            else { return String.Empty.GetHashCode(); }
        }
        #endregion

        public override String ToString()
        {
            if (CatalogName is String) { return CatalogName; }
            else { return String.Empty; }
        }


    }
}
