using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeTitle
    {
        String? AttributeTitle { get; }
    }

    public class DomainAttributeTitle : IDomainAttributeTitle, IEquatable<IDomainAttributeTitle>, IComparable<IDomainAttributeTitle>, IComparable
    {
        public String? AttributeTitle { get; init; }

        public DomainAttributeTitle() : base() { }

        public DomainAttributeTitle(IDomainAttributeTitle source) : base()
        {
            if (source.AttributeTitle is String) { AttributeTitle = source.AttributeTitle; }
            else { AttributeTitle = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDomainAttributeTitle? other)
        {
            return (
                other is IDbSchemaName &&
                !String.IsNullOrEmpty(AttributeTitle) &&
                !String.IsNullOrEmpty(other.AttributeTitle) &&
                AttributeTitle.Equals(other.AttributeTitle, StringComparison.CurrentCultureIgnoreCase));
        }

        public Int32 CompareTo(IDomainAttributeTitle? other)
        {
            if (other is null) { return 1; }
            else { return String.Compare(AttributeTitle, other.AttributeTitle, true); }
        }

        public int CompareTo(object? obj)
        { if (obj is IDomainAttributeTitle value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDomainAttributeTitle value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DomainAttributeTitle left, IDomainAttributeTitle right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeTitle left, IDomainAttributeTitle right)
        { return !left.Equals(right); }

        public static bool operator <(DomainAttributeTitle left, IDomainAttributeTitle right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DomainAttributeTitle left, IDomainAttributeTitle right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DomainAttributeTitle left, IDomainAttributeTitle right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DomainAttributeTitle left, IDomainAttributeTitle right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (AttributeTitle is String)
            { return (AttributeTitle).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (AttributeTitle is String)
            { return String.Format("{0}.{1}", base.ToString(), AttributeTitle); }
            else { return String.Empty; }
        }
    }
}
