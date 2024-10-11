using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IPrincipleValue: ISecurityPrincipleItem, IScopeType,
        IPrincipleIndex, IPrincipleIndexName
    { }

    /// <inheritdoc/>
    public class PrincipleValue : SecurityPrincipleItem, IPrincipleValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.SecurityPrinciple;
    }
}
