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
    public interface IRoleIndex : IRoleKey
    { }

    /// <inheritdoc/>
    public class RoleIndex : RoleKey, IRoleIndex,
        IKeyEquality<IRoleIndex>, IKeyEquality<RoleIndex>
    {
        /// <inheritdoc cref="RoleKey.RoleKey(IRoleKey)"/>
        public RoleIndex(IRoleIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(RoleIndex? other)
        { return other is IRoleKey value && Equals(new RoleKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IRoleIndex? other)
        { return other is IRoleKey value && Equals(new RoleKey(value)); }

        /// <summary>
        /// Convert RoleIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoleIndex source)
        { return new DataIndex() { SystemId = source.RoleId ?? Guid.Empty }; }
    }
}
