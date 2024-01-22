using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ModelData.Attribute
{
    /// <summary>
    /// Interface for the Model Attribute Key
    /// </summary>
    public interface IModelAttributeKey: IModelSubjectAreaKey, IDomainAttributeKey
    { }

    /// <summary>
    /// Implementation for Model Attribute Key
    /// </summary>
    public class ModelAttributeKey : IModelAttributeKey, IKeyEquality<IModelAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Key
        /// </summary>
        /// <param name="source"></param>
        public ModelAttributeKey(IModelAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }

            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IModelAttributeKey? other)
        { return other is IModelAttributeKey key
                && EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId)
                && EqualityComparer<Guid?>.Default.Equals(SubjectAreaId, key.SubjectAreaId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelAttributeKey value && Equals(new ModelAttributeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ModelAttributeKey left, ModelAttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelAttributeKey left, ModelAttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AttributeId); }
        #endregion

    }
}
