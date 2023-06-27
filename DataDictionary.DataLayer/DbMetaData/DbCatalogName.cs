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

    public record DbCatalogName : IDbCatalogName, IEquatable<IDbCatalogName>, IComparable<IDbCatalogName>
    {
        public String? CatalogName { get; init; }

        public DbCatalogName(IDbCatalogName source) : base()
        { CatalogName = source.CatalogName; }

        #region IEquatable, IComparable
        public bool Equals(IDbCatalogName? other)
        {
            return (
                other is IDbCatalogName &&
                !String.IsNullOrEmpty(CatalogName) &&
                !String.IsNullOrEmpty(other.CatalogName) &&
                CatalogName.Equals(other.CatalogName, StringComparison.CurrentCultureIgnoreCase));
        }

        public int CompareTo(IDbCatalogName? other)
        {
            if (other is IDbCatalogName)
            { return String.Compare(CatalogName, other.CatalogName, true); }
            else
            {
                if (String.IsNullOrEmpty(CatalogName)) { return 1; }
                else { return -1; }
            }
        }

        public static bool operator <(DbCatalogName left, DbCatalogName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbCatalogName left, DbCatalogName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbCatalogName left, DbCatalogName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbCatalogName left, DbCatalogName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }
        #endregion

        public override int GetHashCode()
        {
            if (CatalogName is String) { return CatalogName.GetHashCode(); }
            else { return base.GetHashCode(); }
        }
    }
}
