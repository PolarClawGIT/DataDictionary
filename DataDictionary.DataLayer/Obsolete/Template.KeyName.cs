using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the unique Name of a Template.
    /// </summary>
    [Obsolete]
    public interface ITemplateKeyName : IKey
    {
        /// <summary>
        /// Title of the Scripting Template
        /// </summary>
        String? TemplateTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Template.
    /// </summary>
    [Obsolete]
    public class TemplateKeyName : ITemplateKeyName,
        IKeyComparable<ITemplateKeyName>, IKeyComparable<TemplateKeyName>
    {
        /// <inheritdoc/>
        public String TemplateTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Template Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public TemplateKeyName(ITemplateKeyName source) : base()
        {
            if (source.TemplateTitle is string) { TemplateTitle = source.TemplateTitle; }
            else { TemplateTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(TemplateKeyName? other)
        {
            return
                other is TemplateKeyName &&
                !string.IsNullOrEmpty(TemplateTitle) &&
                !string.IsNullOrEmpty(other.TemplateTitle) &&
                TemplateTitle.Equals(other.TemplateTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ITemplateKeyName? other)
        { return other is ITemplateKeyName value && Equals(new TemplateKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateKeyName value && Equals(new TemplateKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(TemplateKeyName? other)
        {
            if (other is TemplateKeyName value)
            { return string.Compare(TemplateTitle, value.TemplateTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ITemplateKeyName? other)
        { if (other is ITemplateKeyName value) { return CompareTo(new TemplateKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ITemplateKeyName value) { return CompareTo(new TemplateKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateKeyName left, TemplateKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateKeyName left, TemplateKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(TemplateKeyName left, TemplateKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(TemplateKeyName left, TemplateKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(TemplateKeyName left, TemplateKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(TemplateKeyName left, TemplateKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return TemplateTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (TemplateTitle is string) { return TemplateTitle; }
            else { return string.Empty; }
        }
    }
}
