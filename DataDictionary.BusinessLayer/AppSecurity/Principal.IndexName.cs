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
    public interface IPrincipalIndexName : IPrincipalKeyName 
    { }

    /// <inheritdoc/>
    public class PrincipalIndexName : PrincipalKeyName, IPrincipalIndexName,
        IKeyEquality<IPrincipalIndexName>, IKeyEquality<PrincipalIndexName>
    {
        /// <inheritdoc cref="PrincipalKey.PrincipalKey(IPrincipalKey)"/>
        public PrincipalIndexName(IPrincipalIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(PrincipalIndexName? other)
        { return other is IPrincipalKey value && Equals(new PrincipalKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IPrincipalIndexName? other)
        { return other is IPrincipalKey value && Equals(new PrincipalKey(value)); }
    }
}
