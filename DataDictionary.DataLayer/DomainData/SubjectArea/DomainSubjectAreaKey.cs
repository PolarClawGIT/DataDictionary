using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.SubjectArea
{
    /// <summary>
    /// Interface for the Domain Subject Area Key
    /// </summary>
    public interface IDomainSubjectAreaKey: IKey
    {
        /// <summary>
        /// Application ID for the Domain Subject Area.
        /// </summary>
        Guid? SubjectAreaId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Subject Area Key
    /// </summary>
    public class DomainSubjectAreaKey : IDomainSubjectAreaKey, IKeyEquality<IDomainSubjectAreaKey>
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain SubjectArea Key
        /// </summary>
        /// <param name="source"></param>
        public DomainSubjectAreaKey(IDomainSubjectAreaKey source) : base()
        {
            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain SubjectArea Key
        /// </summary>
        /// <param name="source"></param>
        public DomainSubjectAreaKey(IDomainEntityItem source) : base()
        {
            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain SubjectArea Key
        /// </summary>
        /// <param name="source"></param>
        public DomainSubjectAreaKey(IDomainAttributeItem source) : base()
        {
            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainSubjectAreaKey? other)
        { return other is IDomainSubjectAreaKey key && EqualityComparer<Guid?>.Default.Equals(SubjectAreaId, key.SubjectAreaId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainSubjectAreaKey value && Equals(new DomainSubjectAreaKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainSubjectAreaKey left, DomainSubjectAreaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainSubjectAreaKey left, DomainSubjectAreaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(SubjectAreaId); }
        #endregion
    }
}
