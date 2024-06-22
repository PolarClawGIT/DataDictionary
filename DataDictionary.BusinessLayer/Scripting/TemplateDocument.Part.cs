using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    public class TemplateDocumentPart : ITemplateNodeIndex, INamedScopeIndex
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; }

        /// <inheritdoc/>
        public Guid NamedScopeId { get; }

        /// <summary>
        /// The XElement for the part.
        /// </summary>
        public XElement? Value { get; set; }

        /// <summary>
        /// The child Document Parts
        /// </summary>
        public List<TemplateDocumentPart> Children { get; } = new List<TemplateDocumentPart>();


    }
}
