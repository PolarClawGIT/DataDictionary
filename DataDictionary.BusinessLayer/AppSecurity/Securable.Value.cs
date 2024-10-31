// Ignore Spelling: Securable

using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface ISecurableValue : ISecurableItem,
        ISecurableIndex, ISecurableIndexName, IDataValue
    { }

    /// <inheritdoc/>
    public class SecurableValue : SecurableItem, ISecurableValue
    {
        IDataValue dataValue;  // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.SecuritySecurable; } }

        /// <inheritdoc/>
        public SecurableValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new SecurableIndex(this),
                GetTitle = () => SecurableTitle ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(SecurableTitle)
            };
        }

    }
}
