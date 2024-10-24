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
    public class SecurableIndexName : SecurableKeyName, ISecurableIndexName,
        IKeyEquality<ISecurableIndexName>, IKeyEquality<SecurableIndexName>
    {
        /// <inheritdoc cref="SecurableKey.SecurableKey(ISecurableKey)"/>
        public SecurableIndexName(ISecurableIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(SecurableIndexName? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ISecurableIndexName? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }
    }
}
