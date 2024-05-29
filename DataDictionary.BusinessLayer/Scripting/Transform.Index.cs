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

    /// <inheritdoc/>
    public interface ITransformIndexName : ITransformKeyName
    { }

    /// <inheritdoc/>
    public class TransformIndexName : TransformKeyName, ITransformIndexName
    {
        /// <inheritdoc cref="TransformKeyName(ITransformKeyName)"/>
        public TransformIndexName(ITransformIndexName source) : base(source) { }
    }
}
