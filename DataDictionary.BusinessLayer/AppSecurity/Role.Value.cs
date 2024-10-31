using DataDictionary.BusinessLayer.ToolSet;
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
    public interface IRoleValue : IRoleItem, IScopeType,
        IRoleIndex, IRoleIndexName, IDataValue
    { }

    /// <inheritdoc/>
    public class RoleValue : RoleItem, IRoleValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.SecurityRole;

        /// <inheritdoc/>
        public DataIndex Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        public String Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public RoleValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new RoleIndex(this),
                GetTitle = () => RoleName ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(RoleName)
            };
        }
    }
}
