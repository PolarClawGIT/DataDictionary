using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for common Domain Alias properties
    /// </summary>
    public interface IDomainAliasItem: IDomainAliasNameKey, IScopeUniqueKey
    {
        /// <summary>
        /// Name of the Source of the Alias.
        /// Name of Database or Assembly.
        /// </summary>
        String? SourceName { get; }
    }
}
