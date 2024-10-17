using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IObjectIndex : IObjectKey
    { }

    /// <inheritdoc/>
    public class ObjectIndex : ObjectKey, IObjectIndex,
        IKeyEquality<IObjectIndex>, IKeyEquality<ObjectIndex>
    {
        /// <inheritdoc cref="ObjectKey.ObjectKey(IObjectKey)"/>
        public ObjectIndex(IObjectIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ObjectIndex? other)
        { return other is IObjectKey value && Equals(new ObjectKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IObjectIndex? other)
        { return other is IObjectKey value && Equals(new ObjectKey(value)); }

        /// <summary>
        /// Convert ObjectIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ObjectIndex source)
        { return new DataIndex() { SystemId = source.ObjectId ?? Guid.Empty }; }
    }
}
