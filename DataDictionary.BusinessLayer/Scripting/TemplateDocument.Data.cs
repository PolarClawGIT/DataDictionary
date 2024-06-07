using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Template Document
    /// </summary>
    public interface ITemplateDocumentData : ICollection<TemplateDocumentValue>, IBindingList
    { }

    class TemplateDocumentData : BindingList<TemplateDocumentValue>, ITemplateDocumentData
    { }
}
