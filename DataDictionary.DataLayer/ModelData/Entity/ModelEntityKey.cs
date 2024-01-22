using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ModelData.Entity
{
    /// <summary>
    /// Interface for the Model Entity Key
    /// </summary>
    public interface IModelEntityKey: IModelSubjectAreaKey, IDomainEntityKey
    { }

    /// <summary>
    /// Implementation for Model Entity Key
    /// </summary>
    public class ModelEntityKey : IModelEntityKey, IKeyEquality<IModelEntityKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Entity Key
        /// </summary>
        /// <param name="source"></param>
        public ModelEntityKey(IModelEntityKey source) : base()
        {
            if (source.EntityId is Guid) { EntityId = source.EntityId; }
            else { EntityId = Guid.Empty; }

            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IModelEntityKey? other)
        { return other is IModelEntityKey key
                && EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId)
                && EqualityComparer<Guid?>.Default.Equals(SubjectAreaId, key.SubjectAreaId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelEntityKey value && Equals(new ModelEntityKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ModelEntityKey left, ModelEntityKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelEntityKey left, ModelEntityKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(EntityId); }
        #endregion

    }
}
