﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    /// <summary>
    /// Parent Key reference to the Primary Key of an Help Item.
    /// </summary>
    public interface IHelpKeyParent
    {
        /// <summary>
        /// Parent Primary Key reference to a Parent Help document.
        /// </summary>
        Nullable<Guid> HelpParentId { get; }
    }

    /// <inheritdoc/>
    public class HelpKeyParent: IHelpKeyParent, IEquatable<HelpKeyParent>, IEquatable<HelpKey>
    {
        /// <inheritdoc/>
        public Nullable<Guid> HelpParentId { get; init; } = Guid.Empty;

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
        { return other is HelpKeyParent && EqualityComparer<Guid?>.Default.Equals(this.HelpParentId, other.HelpParentId); }

        /// <inheritdoc/>
        public Boolean Equals(HelpKey? other)
        { return other is HelpKey && EqualityComparer<Guid?>.Default.Equals(this.HelpParentId, other.HelpId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IHelpKeyParent value && this.Equals(new HelpKeyParent(value)); }

        /// <inheritdoc/>
        public static bool operator ==(HelpKeyParent left, HelpKeyParent right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(HelpKeyParent left, HelpKeyParent right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (HelpParentId is Guid) { return (HelpParentId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}