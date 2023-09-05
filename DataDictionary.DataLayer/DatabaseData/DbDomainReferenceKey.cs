using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbDomainReferenceKey
    {
        String? DomainCatalog { get; }
        String? DomainSchema { get; }
        String? DomainName { get; }
    }

    public class DbDomainReferenceKey: IDbDomainReferenceKey, IEquatable<DbDomainReferenceKey>, IComparable<DbDomainReferenceKey>, IComparable
    {
        public String DomainCatalog { get; init; } 
        public String DomainSchema { get; init; } 
        public String DomainName { get; init; } 

        public DbDomainReferenceKey(IDbDomainReferenceKey source) :base()
        {
            if (source.DomainCatalog is String) { DomainCatalog = source.DomainCatalog; }
            else { DomainCatalog = String.Empty; }

            if (source.DomainSchema is String) { DomainSchema = source.DomainSchema; }
            else { DomainSchema = String.Empty; }

            if (source.DomainName is String) { DomainName = source.DomainName; }
            else { DomainName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbDomainReferenceKey? other)
        {
            return (
                other is IDbDomainReferenceKey &&
                !String.IsNullOrEmpty(DomainCatalog) &&
                !String.IsNullOrEmpty(other.DomainCatalog) &&
                !String.IsNullOrEmpty(DomainSchema) &&
                !String.IsNullOrEmpty(other.DomainSchema) &&
                !String.IsNullOrEmpty(DomainName) &&
                !String.IsNullOrEmpty(other.DomainName) &&
                DomainCatalog.Equals(other.DomainCatalog, ModelFactory.CompareString) &&
                DomainSchema.Equals(other.DomainSchema, ModelFactory.CompareString) &&
                DomainName.Equals(other.DomainName, ModelFactory.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDbDomainReferenceKey value && this.Equals(new DbDomainReferenceKey(value)); }

        public Int32 CompareTo(DbDomainReferenceKey? other)
        {
            if (other is null) { return 1; }
            else
            {
                if (String.Compare(DomainCatalog,
                                   other.DomainCatalog,
                                   true) is Int32 catalogValue && catalogValue != 0)
                { return catalogValue; }
                else if (String.Compare(DomainSchema,
                                   other.DomainSchema,
                                   true) is Int32 schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return String.Compare(DomainName, other.DomainName, true); }
            }
        }

        public virtual int CompareTo(object? obj)
        { if (obj is IDbDomainReferenceKey value) { return this.CompareTo(new DbDomainReferenceKey(value)); } else { return 1; } }

        public static bool operator ==(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbDomainReferenceKey left, DbDomainReferenceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(DomainCatalog, DomainSchema, DomainName); }
        #endregion

        public override string ToString()
        {
            if (DomainCatalog is String && DomainSchema is String && DomainName is String)
            { return String.Format("{0}.{1}.{2}", DomainCatalog, DomainSchema, DomainName); }
            else { return String.Empty; }
        }


    }
}
