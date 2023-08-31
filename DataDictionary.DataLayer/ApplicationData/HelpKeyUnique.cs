using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IHelpKeyUnique
    {
        String? NameSpace { get; }
    }

    public class HelpKeyUnique : IHelpKeyUnique, IEquatable<HelpKeyUnique>, IComparable<HelpKeyUnique>, IComparable
    {
        public String NameSpace { get; init; } = String.Empty;

        public HelpKeyUnique(IHelpKeyUnique source) : base()
        {
            if (source.NameSpace is String) { NameSpace = source.NameSpace; }
            else { NameSpace = String.Empty; }
        }

        public HelpKeyUnique(Object source) : base()
        {
            if (source.GetType().FullName is String value) { NameSpace = value; }
            else { NameSpace = String.Empty; }
        }

        #region IEquatable, IComparable
        public virtual bool Equals(HelpKeyUnique? other)
        {
            return (
                other is HelpKeyUnique &&
                !String.IsNullOrEmpty(NameSpace) &&
                !String.IsNullOrEmpty(other.NameSpace) &&
                NameSpace.Equals(other.NameSpace, ModelFactory.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IHelpKeyUnique value && this.Equals(new HelpKeyUnique(value)); }

        public virtual int CompareTo(HelpKeyUnique? other)
        {
            if (other is HelpKeyUnique value)
            { return String.Compare(NameSpace, value.NameSpace, true); }
            else { return 1; }
        }

        public virtual int CompareTo(object? obj)
        { if (obj is IHelpKeyUnique value) { return this.CompareTo(new HelpKeyUnique(value)); } else { return 1; } }

        public static bool operator ==(HelpKeyUnique left, HelpKeyUnique right)
        { return left.Equals(right); }

        public static bool operator !=(HelpKeyUnique left, HelpKeyUnique right)
        { return !left.Equals(right); }

        public static bool operator <(HelpKeyUnique left, HelpKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(HelpKeyUnique left, HelpKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(HelpKeyUnique left, HelpKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(HelpKeyUnique left, HelpKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(NameSpace); }
        #endregion

        public override String ToString()
        {
            if (NameSpace is String) { return NameSpace; }
            else { return String.Empty; }
        }
    }
}
