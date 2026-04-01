using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITransformIndex : ITransformKey
    { }

    /// <inheritdoc/>
    public class TransformIndex : TransformKey, ITransformIndex,
        IKeyEquality<ITransformIndex>, IKeyEquality<TransformIndex>
    {
        /// <inheritdoc cref="TransformKey(ITransformKey)"/>
        public TransformIndex(ITransformIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITransformIndex? other)
        { return other is ITransformKey key && Equals(new TransformKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TransformIndex? other)
        { return other is ITransformKey key && Equals(new TransformKey(key)); }

        /// <summary>
        /// Convert TransformIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TransformIndex source)
        { return new DataIndex() { SystemId = source.TransformId ?? Guid.Empty }; }
    }
}
