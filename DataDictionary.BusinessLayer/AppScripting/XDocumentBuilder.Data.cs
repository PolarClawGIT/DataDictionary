using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Template Document
    /// </summary>
    public interface IXDocumentData :
        ICollection<XDocumentValue>, IBindingList<XDocumentValue>
    {
        /// <summary>
        /// Removes all occurrences of items that match.
        /// </summary>
        /// <param name="template"></param>
        /// <returns>False if any of the items could not be removed.</returns>
        Boolean Remove(IScriptingTemplateIndex template);
    }

    class XDocumentData : BindingList<XDocumentValue>, IXDocumentData
    {
        /// <inheritdoc/>
        public Boolean Remove(IScriptingTemplateIndex template)
        {
            ScriptingTemplateIndex key = new ScriptingTemplateIndex(template);
            Boolean result = true;

            while (result && this.FirstOrDefault(w => key.Equals(w)) is XDocumentValue value)
            { result = base.Remove(value); }

            return result;
        }
    }
}
