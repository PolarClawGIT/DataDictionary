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
    public interface IPrincipleIndexName : IPrincipleKeyName 
    { }

    /// <inheritdoc/>
    public class PrincipleIndexName : PrincipleKeyName, IPrincipleIndexName,
        IKeyEquality<IPrincipleIndexName>, IKeyEquality<PrincipleIndexName>
    {
        /// <inheritdoc cref="PrincipleKey.PrincipleKey(IPrincipleKey)"/>
        public PrincipleIndexName(IPrincipleIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(PrincipleIndexName? other)
        { return other is IPrincipleKey value && Equals(new PrincipleKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IPrincipleIndexName? other)
        { return other is IPrincipleKey value && Equals(new PrincipleKey(value)); }
    }
}
