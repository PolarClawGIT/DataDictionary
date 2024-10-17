using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
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
    public interface IHelpSecurityValue : IHelpSecurityItem,
        IHelpSubjectIndex, IPrincipleIndex, IRoleIndex, IScopeType,
        IObjectValue
    { }

    /// <inheritdoc/>
    public class HelpSecurityValue: HelpSecurityItem, IHelpSecurityValue
    {
        IObjectValue securityValue; // Backing field for IObjectSecurityValue

        /// <inheritdoc cref="HelpSecurityItem.HelpSecurityItem()"/>
        public HelpSecurityValue() : base() 
        {
            securityValue = ObjectValue.Create(new DataValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            }, this);
        }

        /// <inheritdoc cref="HelpSecurityItem.HelpSecurityItem(IHelpKeyItem, IPrincipleKey)"/>
        public HelpSecurityValue(IHelpKeyItem helpItem, IPrincipleIndex principleIndex) : base (helpItem, principleIndex)
        {
            securityValue = ObjectValue.Create(new DataValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            }, this);
        }

        /// <inheritdoc cref="HelpSecurityItem.HelpSecurityItem(IHelpKeyItem, IRoleKey)"/>
        public HelpSecurityValue(IHelpKeyItem helpItem, IRoleIndex roleIndex) : base(helpItem, roleIndex)
        {
            securityValue = ObjectValue.Create(new DataValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            }, this);
        }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.SecurityHelp; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return securityValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return securityValue.Title; } }
    }
}
