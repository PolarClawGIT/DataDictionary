using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITransformIndexName : ITransformKeyName
    { }

    /// <inheritdoc/>
    public class TransformIndexName : TransformKeyName, ITransformIndexName,
        IKeyEquality<ITransformIndexName>, IKeyEquality<TransformIndexName>
    {
        /// <inheritdoc cref="TransformKeyName(ITransformKeyName)"/>
        public TransformIndexName(ITransformIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITransformIndexName? other)
        { return other is ITransformKeyName key && Equals(new TransformKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TransformIndexName? other)
        { return other is ITransformKeyName key && Equals(new TransformKeyName(key)); }

        /// <summary>
        /// Convert TransformIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TransformIndexName source)
        { return new DataIndexName() { Title = source.TransformTitle ?? String.Empty }; }
    }
}
