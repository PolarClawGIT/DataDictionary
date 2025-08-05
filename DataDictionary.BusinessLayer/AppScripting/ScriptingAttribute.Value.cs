using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.DataLayer.AppScript;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public interface IScriptingAttributeValue : IScriptingAttributeItem, IScriptingTemplateIndex, IScriptingNodeIndex, IPropertyIndex
    { }

    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public class ScriptingAttributeValue : ScriptingAttributeItem, IScriptingAttributeValue
    {
        /// <inheritdoc/>
        public ScriptingAttributeValue() : base() { }

        /// <inheritdoc cref="ScriptingAttributeItem(IScriptingNodeKeyComposite)"/>
        public ScriptingAttributeValue(IScriptingNodeKeyComposite source) : base(source)
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

            if(String.IsNullOrWhiteSpace(valueString)) { return null; }
            return new XAttribute(name, valueString);
        }
    }
}
