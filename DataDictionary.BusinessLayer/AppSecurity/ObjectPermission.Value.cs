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
    public interface IObjectPermissionValue : IObjectPermissionItem,
        IRoleIndex, IObjectIndex, IScopeType
    { }

    /// <inheritdoc/>
    public class ObjectPermissionValue : ObjectPermissionItem, IObjectPermissionValue
    {
        /// <inheritdoc/>
        public ObjectPermissionValue() : base() { }

        /// <inheritdoc cref="ObjectPermissionItem.ObjectPermissionItem(IRoleKey)"/>
        public ObjectPermissionValue(IRoleIndex roleIndex): base(roleIndex)
        { }

        /// <inheritdoc cref="ObjectPermissionItem.ObjectPermissionItem(IRoleKey, IObjectKey)"/>
        public ObjectPermissionValue(IRoleIndex roleIndex, IObjectIndex objectIndex): base(roleIndex, objectIndex) 
        { }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.SecurityPermission; } }
    }
}
