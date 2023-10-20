using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.SubjectArea
{
    /// <summary>
    /// Interface for the unique Name of a SubjectArea.
    /// </summary>
    public interface IDomainSubjectAreaKeyUnique : IKey
    {
        /// <summary>
        /// Title of the Domain SubjectArea (aka Name of the SubjectArea)
        /// </summary>
        String? SubjectAreaTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a SubjectArea.
    /// </summary>
    public class DomainSubjectAreaKeyUnique : IDomainSubjectAreaKeyUnique, IKeyComparable<IDomainSubjectAreaKeyUnique>
    {
        /// <inheritdoc/>
        public String SubjectAreaTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the SubjectArea Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainSubjectAreaKeyUnique(IDomainSubjectAreaKeyUnique source) : base()
        {
            if (source.SubjectAreaTitle is string) { SubjectAreaTitle = source.SubjectAreaTitle; }
            else { SubjectAreaTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainSubjectAreaKeyUnique? other)
        {
            return
                other is IDomainSubjectAreaKeyUnique &&
                !string.IsNullOrEmpty(SubjectAreaTitle) &&
                !string.IsNullOrEmpty(other.SubjectAreaTitle) &&
                SubjectAreaTitle.Equals(other.SubjectAreaTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainSubjectAreaKeyUnique value && Equals(new DomainSubjectAreaKeyUnique(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainSubjectAreaKeyUnique? other)
        {
            if (other is DomainSubjectAreaKeyUnique value)
            { return string.Compare(SubjectAreaTitle, value.SubjectAreaTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainSubjectAreaKeyUnique value) { return CompareTo(new DomainSubjectAreaKeyUnique(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainSubjectAreaKeyUnique left, DomainSubjectAreaKeyUnique right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainSubjectAreaKeyUnique left, DomainSubjectAreaKeyUnique right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainSubjectAreaKeyUnique left, DomainSubjectAreaKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainSubjectAreaKeyUnique left, DomainSubjectAreaKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainSubjectAreaKeyUnique left, DomainSubjectAreaKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainSubjectAreaKeyUnique left, DomainSubjectAreaKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return SubjectAreaTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (SubjectAreaTitle is string) { return SubjectAreaTitle; }
            else { return string.Empty; }
        }
    }
}