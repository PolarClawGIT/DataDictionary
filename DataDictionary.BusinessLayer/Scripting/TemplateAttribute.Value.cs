using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.BusinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateAttributeValue : IScriptingAttributeItem, ITemplateIndex, ITemplateNodeIndex, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class TemplateAttributeValue : ScriptingAttributeItem, ITemplateAttributeValue
    {
        /// <inheritdoc/>
        public TemplateAttributeValue() : base() { }

        /// <inheritdoc cref="ScriptingAttributeItem(IScriptingNodeKeyComposite)"/>
        public TemplateAttributeValue(IScriptingNodeKeyComposite source) : base(source)
        { }

        internal XAttribute? BuildXAttribute(String name, Object? value)
        {
            if (String.IsNullOrWhiteSpace(name)) { return null; }

            try { name = XmlConvert.VerifyName(name); } // Throws an exception if the name is not valid.
            catch (Exception ex)
            {
                ex.Data.Add(nameof(name), name);
                throw;
            }

            if (value is null) { return null; }
            String valueString = (value.ToString()??String.Empty).Trim();

            if(this.AsCData == true) { return new XAttribute(name, new XCData(valueString)); }
            else { return new XAttribute(name, valueString); }
        }
    }
}
