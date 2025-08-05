using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateValue : ITemplateItem, ITemplateIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TemplateValue : TemplateItem, ITemplateValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public TemplateValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateIndex(this),
                GetScope = () => Scope,
                GetTitle = () => TemplateTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsTitleChanged = (e) => e.PropertyName is nameof(TemplateTitle)
            };
        }
    }
}
