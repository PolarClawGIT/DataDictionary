using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Parent Key reference to the Primary Key of an Help Item.
    /// </summary>
    public interface IHelpKeyParent : IKey
    {
        /// <summary>
        /// Parent Primary Key reference to a Parent Help document.
        /// </summary>
        Guid? HelpParentId { get; }
    }

    /// <inheritdoc/>
    public class HelpKeyParent : IHelpKeyParent,
        IKeyEquality<IHelpKeyParent>, IKeyEquality<IHelpKey>,
        IKeyEquality<HelpKeyParent>
    {
        /// <inheritdoc/>
        public Guid? HelpParentId { get; init; } = Guid.Empty;

        /// <summary>
        /// Creates a Help Key from a item that implements the Primary key.
        /// </summary>
        /// <param name="source"></param>
        public HelpKeyParent(IHelpKeyParent source) : base()
        {
            if (source.HelpParentId is Guid) { HelpParentId = source.HelpParentId; }
            else { HelpParentId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(HelpKeyParent? other)
        { return other is HelpKeyParent && EqualityComparer<Guid?>.Default.Equals(HelpParentId, other.HelpParentId); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpKeyParent? other)
        { return other is IHelpKeyParent value && Equals(new HelpKeyParent(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpKey? other)
        { return other is IHelpKey && EqualityComparer<Guid?>.Default.Equals(HelpParentId, other.HelpId); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IHelpKeyParent value && Equals(new HelpKeyParent(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(HelpKeyParent left, HelpKeyParent right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HelpKeyParent left, HelpKeyParent right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (HelpParentId is Guid) { return HelpParentId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }

        #endregion
    }
}
