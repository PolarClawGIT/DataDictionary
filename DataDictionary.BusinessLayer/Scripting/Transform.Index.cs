using DataDictionary.DataLayer.ScriptingData.Transform;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITransformIndex : ITransformKey
    { }

    /// <inheritdoc/>
    public class TransformIndex : TransformKey, ITransformIndex
    {
        /// <inheritdoc cref="TransformKey(ITransformKey)"/>
        public TransformIndex(ITransformIndex source) : base(source) { }

        /// <summary>
        /// Convert TransformIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(TransformIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.TransformId ?? Guid.Empty }; }
    }
}
