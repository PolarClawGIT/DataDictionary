using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface IModelIndex : IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : ModelKey, IModelIndex,
        IKeyEquality<IModelIndex>, IKeyEquality<ModelIndex>
    {
        /// <inheritdoc cref="ModelKey.ModelKey(IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IModelIndex? other)
        { return other is IModelKey key && Equals(new ModelKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ModelIndex? other)
        { return other is IModelKey key && Equals(new ModelKey(key)); }

        /// <summary>
        /// Convert ModelIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(ModelIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.ModelId ?? Guid.Empty }; }
    }
}
