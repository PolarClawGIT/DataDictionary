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
    public interface IPrincipalValue: IPrincipalItem, IScopeType,
        IPrincipalIndex, IPrincipalIndexName
    { }

    /// <inheritdoc/>
    public class PrincipalValue : PrincipalItem, IPrincipalValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.SecurityPrincipal;
    }
}
