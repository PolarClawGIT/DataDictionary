using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Help
{
    /// <summary>
    /// Primary key for the Help Documentation.
    /// </summary>
    public interface IHelpKey : IKey
    {
        /// <summary>
        /// Primary Key ID for the Help Documentation.
        /// </summary>
        Guid? HelpId { get; }
    }

    /// <summary>
    /// Primary key for the Help Documentation.
    /// </summary>
    public class HelpKey : IHelpKey, IKeyEquality<HelpKey>
    {
        /// <inheritdoc/>
        public Guid? HelpId { get; init; } = Guid.Empty;

        /// <summary>
        /// Creates a Help Key from a item that implements the Primary key.
        /// </summary>
        /// <param name="source"></param>
        public HelpKey(IHelpKey source) : base()
        {
            if (source.HelpId is Guid) { HelpId = source.HelpId; }
            else { HelpId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(HelpKey? other)
        { return other is HelpKey && EqualityComparer<Guid?>.Default.Equals(HelpId, other.HelpId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IHelpKey value && Equals(new HelpKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(HelpKey left, HelpKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(HelpKey left, HelpKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (HelpId is Guid) { return HelpId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
