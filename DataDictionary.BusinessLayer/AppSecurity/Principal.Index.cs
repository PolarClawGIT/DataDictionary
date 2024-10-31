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
    public interface IPrincipalIndex : IPrincipalKey
    { }

    /// <inheritdoc/>
    public class PrincipalIndex : PrincipalKey, IPrincipalIndex,
        IKeyEquality<IPrincipalIndex>, IKeyEquality<PrincipalIndex>
    {
        /// <inheritdoc cref="PrincipalKey.PrincipalKey(IPrincipalKey)"/>
        public PrincipalIndex(IPrincipalIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(PrincipalIndex? other)
        { return other is IPrincipalKey value && Equals(new PrincipalKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IPrincipalIndex? other)
        { return other is IPrincipalKey value && Equals(new PrincipalKey(value)); }

        /// <summary>
        /// Convert PrincipalIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(PrincipalIndex source)
        { return new DataIndex() { SystemId = source.PrincipalId ?? Guid.Empty }; }
    }
}
