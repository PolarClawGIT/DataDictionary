﻿using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Help
{
    /// <summary>
    /// Unique Key for Help Documents, by NameSpace
    /// </summary>
    public interface IHelpKeyNameSpace: IKey
    {
        /// <summary>
        /// Key to reference a Help Document by Name Space
        /// </summary>
        string? NameSpace { get; }
    }

    /// <summary>
    /// Unique Key for Help Documents, by NameSpace
    /// </summary>
    public class HelpKeyNameSpace : IHelpKeyNameSpace, IKeyComparable<IHelpKeyNameSpace>
    {
        /// <inheritdoc/>
        public string NameSpace { get; init; } = string.Empty;

        /// <summary>
        /// Create a Help Key by NameSpace that implement the Unique Key
        /// </summary>
        /// <param name="source"></param>
        public HelpKeyNameSpace(IHelpKeyNameSpace source) : base()
        {
            if (source.NameSpace is string) { NameSpace = source.NameSpace; }
            else { NameSpace = string.Empty; }
        }

        /// <summary>
        /// Create a Help Key from a Object. Uses the Objects Full Name.
        /// </summary>
        /// <param name="source"></param>
        public HelpKeyNameSpace(object source) : base()
        {
            if (source.GetType().FullName is string value) { NameSpace = value; }
            else { NameSpace = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IHelpKeyNameSpace? other)
        {
            return 
                other is IHelpKeyNameSpace &&
                !string.IsNullOrEmpty(NameSpace) &&
                !string.IsNullOrEmpty(other.NameSpace) &&
                NameSpace.Equals(other.NameSpace, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IHelpKeyNameSpace value && Equals(new HelpKeyNameSpace(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IHelpKeyNameSpace? other)
        {
            if (other is HelpKeyNameSpace value)
            { return string.Compare(NameSpace, value.NameSpace, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IHelpKeyNameSpace value) { return CompareTo(new HelpKeyNameSpace(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(HelpKeyNameSpace left, HelpKeyNameSpace right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(HelpKeyNameSpace left, HelpKeyNameSpace right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(HelpKeyNameSpace left, HelpKeyNameSpace right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(HelpKeyNameSpace left, HelpKeyNameSpace right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(HelpKeyNameSpace left, HelpKeyNameSpace right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(HelpKeyNameSpace left, HelpKeyNameSpace right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return NameSpace.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (NameSpace is string) { return NameSpace; }
            else { return string.Empty; }
        }
    }
}
