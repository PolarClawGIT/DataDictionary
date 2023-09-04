using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IDefinitionKeyUnique
    {
        String? DefinitionTitle { get; }
    }

    public class DefinitionKeyUnique : IDefinitionKeyUnique, IEquatable<DefinitionKeyUnique>, IComparable<DefinitionKeyUnique>, IComparable
    {
        public String DefinitionTitle { get; init; } = String.Empty;

        public DefinitionKeyUnique(IDefinitionKeyUnique source) : base()
        {
            if (source.DefinitionTitle is String) { DefinitionTitle = source.DefinitionTitle; }
            else { DefinitionTitle = String.Empty; }
        }

        #region IEquatable, IComparable
        public virtual bool Equals(DefinitionKeyUnique? other)
        {
            return (
                other is DefinitionKeyUnique &&
                !String.IsNullOrEmpty(DefinitionTitle) &&
                !String.IsNullOrEmpty(other.DefinitionTitle) &&
                DefinitionTitle.Equals(other.DefinitionTitle, ModelFactory.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDefinitionKeyUnique value && this.Equals(new DefinitionKeyUnique(value)); }

        public virtual int CompareTo(DefinitionKeyUnique? other)
        {
            if (other is DefinitionKeyUnique value)
            { return String.Compare(DefinitionTitle, value.DefinitionTitle, true); }
            else { return 1; }
        }

        public virtual int CompareTo(object? obj)
        { if (obj is IDefinitionKeyUnique value) { return this.CompareTo(new DefinitionKeyUnique(value)); } else { return 1; } }

        public static bool operator ==(DefinitionKeyUnique left, DefinitionKeyUnique right)
        { return left.Equals(right); }

        public static bool operator !=(DefinitionKeyUnique left, DefinitionKeyUnique right)
        { return !left.Equals(right); }

        public static bool operator <(DefinitionKeyUnique left, DefinitionKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DefinitionKeyUnique left, DefinitionKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DefinitionKeyUnique left, DefinitionKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DefinitionKeyUnique left, DefinitionKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(DefinitionTitle); }
        #endregion

        public override String ToString()
        {
            if (DefinitionTitle is String) { return DefinitionTitle; }
            else { return String.Empty; }
        }
    }
}
