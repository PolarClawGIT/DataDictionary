using DataDictionary.DataLayer.ModelData;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface IModelIndex : IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : ModelKey, IModelIndex
    {
        /// <inheritdoc cref="ModelKey.ModelKey(IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source) { }

        /// <summary>
        /// Convert ModelIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(ModelIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.ModelId ?? Guid.Empty }; }
    }
}
