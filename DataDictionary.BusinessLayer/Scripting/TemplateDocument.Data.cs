using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Template Document
    /// </summary>
    public interface ITemplateDocumentData :
        ICollection<TemplateDocumentValue>, IBindingList<TemplateDocumentValue>
    {
        /// <summary>
        /// Removes all occurrences of items that match.
        /// </summary>
        /// <param name="template"></param>
        /// <returns>False if any of the items could not be removed.</returns>
        Boolean Remove(ITemplateIndex template);
    }

    class TemplateDocumentData : BindingList<TemplateDocumentValue>, ITemplateDocumentData
    {
        /// <inheritdoc/>
        public Boolean Remove(ITemplateIndex template)
        {
            TemplateIndex key = new TemplateIndex(template);
            Boolean result = true;

            while (result && this.FirstOrDefault(w => key.Equals(w)) is TemplateDocumentValue value)
            { result = base.Remove(value); }

            return result;
        }
    }
}
