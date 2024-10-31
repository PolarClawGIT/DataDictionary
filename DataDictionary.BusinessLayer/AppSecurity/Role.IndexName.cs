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
    public interface IRoleIndexName : IRoleKeyName
    { }

    /// <inheritdoc/>
    public class RoleIndexName : RoleKeyName, IRoleIndexName,
        IKeyEquality<IRoleIndexName>, IKeyEquality<RoleIndexName>
    {
        /// <inheritdoc cref="RoleKey.RoleKey(IRoleKey)"/>
        public RoleIndexName(IRoleIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(RoleIndexName? other)
        { return other is IRoleKey value && Equals(new RoleKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IRoleIndexName? other)
        { return other is IRoleKey value && Equals(new RoleKey(value)); }
    }
}
