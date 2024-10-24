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
    public class SecurableIndex : SecurableKey, ISecurableIndex,
        IKeyEquality<ISecurableIndex>, IKeyEquality<SecurableIndex>
    {

        /// <inheritdoc/>
        protected SecurableIndex(): base() { }

        /// <inheritdoc cref="SecurableKey.SecurableKey(ISecurableKey)"/>
        public SecurableIndex(ISecurableIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(SecurableIndex? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ISecurableIndex? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <summary>
        /// Convert SecurableIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SecurableIndex source)
        { return new DataIndex() { SystemId = source.SecurableId ?? Guid.Empty }; }

        /// <summary>
        /// Convert DataIndex to a SecurableIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator SecurableIndex(DataIndex source)
        { return new SecurableIndex() { SecurableId = source.SystemId }; }
    }
}
