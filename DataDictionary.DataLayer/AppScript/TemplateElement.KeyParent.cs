using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Parent Key for the Scripting Template Element.
    /// </summary>
    [Obsolete]
    public interface ITemplateElementKeyParent : IKey
    {
        /// <summary>
        /// Element ID of the Parent Element. Null = Root Element.
        /// </summary>
        Guid? ParentElementId { get; }
    }

    /// <summary>
    /// Implementation for the Parent Key for the Scripting Template Element.
    /// </summary>
    [Obsolete]
    public class TemplateElementKeyParent : ITemplateElementKeyParent, ITemplateElementKey,
        IKeyEquality<ITemplateElementKeyParent>, IKeyEquality<ITemplateElementKey>,
        IKeyEquality<TemplateElementKeyParent>
    {
        /// <inheritdoc/>
        public Guid? ParentElementId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        Guid? ITemplateElementKey.ElementId { get { return ParentElementId; } }

        /// <summary>
        /// Creates Template Element Parent Keye from a item that implements the Primary key.
        /// </summary>
        /// <param name="source"></param>
        public TemplateElementKeyParent(ITemplateElementKeyParent source) : base()
        {
            if (source.ParentElementId is Guid) { ParentElementId = source.ParentElementId; }
            else { ParentElementId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateElementKeyParent? other)
        { return other is TemplateElementKeyParent key && key.ParentElementId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(ParentElementId, other.ParentElementId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateElementKeyParent? other)
        { return other is ITemplateElementKeyParent value && Equals(new TemplateElementKeyParent(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateElementKey? other)
        { return other is ITemplateElementKey key && key.ElementId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(ParentElementId, key.ElementId); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateElementKeyParent value && Equals(new TemplateElementKeyParent(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateElementKeyParent left, TemplateElementKeyParent right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateElementKeyParent left, TemplateElementKeyParent right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (ParentElementId is Guid) { return ParentElementId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }

        #endregion
    }
}
