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
    }
}
