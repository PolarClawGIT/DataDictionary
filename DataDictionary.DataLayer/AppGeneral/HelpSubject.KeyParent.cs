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
    public interface IHelpSubjectKeyParent : IKey
    {
        /// <summary>
        /// Parent Primary Key reference to a Parent Help document.
        /// </summary>
        Guid? HelpParentId { get; }
    }

    /// <inheritdoc/>
    public class HelpSubjectKeyParent : IHelpSubjectKeyParent,
        IKeyEquality<IHelpSubjectKeyParent>, IKeyEquality<IHelpSubjectKey>,
        IKeyEquality<HelpSubjectKeyParent>
    {
        /// <inheritdoc/>
        public Guid? HelpParentId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return HelpParentId.HasValue && HelpParentId != Guid.Empty; } }

        /// <summary>
        /// Creates a Help Key from a item that implements the Primary key.
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectKeyParent(IHelpSubjectKeyParent source) : base()
        {
            if (source.HelpParentId is Guid) { HelpParentId = source.HelpParentId; }
            else { HelpParentId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectKeyParent? other)
        {
            return other is HelpSubjectKeyParent key
                && HelpParentId.HasValue && HelpParentId != Guid.Empty
                && key.HelpParentId.HasValue && key.HelpParentId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(HelpParentId, other.HelpParentId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectKeyParent? other)
        { return other is IHelpSubjectKeyParent value && Equals(new HelpSubjectKeyParent(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectKey? other)
        { return other is IHelpSubjectKey && EqualityComparer<Guid?>.Default.Equals(HelpParentId, other.HelpId); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IHelpSubjectKeyParent value && Equals(new HelpSubjectKeyParent(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(HelpSubjectKeyParent left, HelpSubjectKeyParent right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HelpSubjectKeyParent left, HelpSubjectKeyParent right)
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
