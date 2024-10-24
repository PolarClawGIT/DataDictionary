// Ignore Spelling: Securable

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
    public interface ISecurableIndexName : ISecurableKeyName
    { }

    /// <inheritdoc/>
    public class ObjectIndexName : SecurableKeyName, ISecurableIndexName,
        IKeyEquality<ISecurableIndexName>, IKeyEquality<ObjectIndexName>
    {
        /// <inheritdoc cref="SecurableKey.SecurableKey(ISecurableKey)"/>
        public ObjectIndexName(ISecurableIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ObjectIndexName? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ISecurableIndexName? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }
    }
}
