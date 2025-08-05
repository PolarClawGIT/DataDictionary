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
    public interface ITemplateNodeOwnerValue : ITemplateNodeOwnerItem,
        ITemplateElementIndex, ITemplateAttributeIndex, ITemplateIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class TemplateNodeOwnerValue : TemplateNodeOwnerItem, ITemplateNodeOwnerValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public TemplateNodeOwnerValue() : base()
        { }
    }
}
