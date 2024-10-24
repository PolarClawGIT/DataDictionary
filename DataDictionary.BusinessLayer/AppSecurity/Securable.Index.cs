// Ignore Spelling: Securable

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
    public interface ISecurableIndex : ISecurableKey
    { }

    /// <inheritdoc/>
    public class ObjectIndex : SecurableKey, ISecurableIndex,
        IKeyEquality<ISecurableIndex>, IKeyEquality<ObjectIndex>
    {

        /// <inheritdoc/>
        protected ObjectIndex(): base() { }

        /// <inheritdoc cref="SecurableKey.SecurableKey(ISecurableKey)"/>
        public ObjectIndex(ISecurableIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ObjectIndex? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ISecurableIndex? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <summary>
        /// Convert ObjectIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ObjectIndex source)
        { return new DataIndex() { SystemId = source.SecurableId ?? Guid.Empty }; }

        /// <summary>
        /// Convert DataIndex to a ObjectIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator ObjectIndex(DataIndex source)
        { return new ObjectIndex() { SecurableId = source.SystemId }; }
    }
}
