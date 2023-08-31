using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IHelpKey
    {
        Nullable<Guid> HelpId { get; }
    }

    public class HelpKey : IHelpKey, IEquatable<HelpKey>
    {
        public Nullable<Guid> HelpId { get; init; } = Guid.Empty;

        public HelpKey(IHelpKey source) : base()
        {
            if (source.HelpId is Guid) { HelpId = source.HelpId; }
            else { HelpId = Guid.Empty; }
        }

        #region IEquatable
        public Boolean Equals(HelpKey? other)
        { return other is HelpKey && EqualityComparer<Guid?>.Default.Equals(this.HelpId, other.HelpId); }

        public override bool Equals(object? obj)
        { return obj is IHelpKey value && this.Equals(new HelpKey(value)); }

        public static bool operator ==(HelpKey left, HelpKey right)
        { return left.Equals(right); }

        public static bool operator !=(HelpKey left, HelpKey right)
        { return !left.Equals(right); }

        public override Int32 GetHashCode()
        {
            if (HelpId is Guid) { return (HelpId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
