using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface for the NameSpace Index
    /// </summary>
    public interface ISubjectNameSpaceIndex : IKey
    {
        /// <summary>
        /// Id Used for the NameSpace
        /// </summary>
        Guid? NameSpaceId { get; }
    }

    /// <summary>
    /// Implementation of the NameSpace Index
    /// </summary>
    public class SubjectNameSpaceIndex: ISubjectNameSpaceIndex, IKeyEquality<ISubjectNameSpaceIndex>
    {
        /// <inheritdoc/>
        public Guid? NameSpaceId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the NameSpace Index
        /// </summary>
        /// <param name="source"></param>
        public SubjectNameSpaceIndex(ISubjectNameSpaceIndex source) : base()
        {
            if (source.NameSpaceId is Guid value) { NameSpaceId = value; }
            else { NameSpaceId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(ISubjectNameSpaceIndex? other)
        { return other is ISubjectNameSpaceIndex key && EqualityComparer<Guid?>.Default.Equals(NameSpaceId, key.NameSpaceId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISubjectNameSpaceIndex value && Equals(new SubjectNameSpaceIndex(value)); }

        /// <inheritdoc/>
        public static bool operator ==(SubjectNameSpaceIndex left, SubjectNameSpaceIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SubjectNameSpaceIndex left, SubjectNameSpaceIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(NameSpaceId); }
        #endregion
    }
}
