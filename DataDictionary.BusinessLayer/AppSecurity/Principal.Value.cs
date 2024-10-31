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
    public interface IPrincipalValue: IPrincipalItem, IScopeType,
        IPrincipalIndex, IPrincipalIndexName, IDataValue
    { }

    /// <inheritdoc/>
    public class PrincipalValue : PrincipalItem, IPrincipalValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.SecurityPrincipal;

        /// <inheritdoc/>
        public DataIndex Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        public String Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public PrincipalValue() : base()
        {
            dataValue =  new DataValue(this)
            {
                GetIndex = () => new PrincipalIndex(this),
                GetTitle = () => PrincipalName ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(PrincipalName)
            };
        }
    }
}
