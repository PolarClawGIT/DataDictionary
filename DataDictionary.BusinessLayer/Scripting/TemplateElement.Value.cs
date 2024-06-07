using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateElementValue: IScriptingElementItem, ITemplateIndex
    { }

    /// <inheritdoc/>
    public class TemplateElementValue : ScriptingElementItem, ITemplateElementValue
    {
        /// <inheritdoc/>
        public TemplateElementValue() : base() { }

        /// <inheritdoc cref="ScriptingElementItem(IScriptingTemplateKey)"/>
        public TemplateElementValue(ITemplateIndex source) : base(source)
        { }

        /// <summary>
        /// Uses the value provided to generate an XML element.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public XElement GetXElement(Object? value)
        {
            String elementName = String.Empty;
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            String stringValue = String.Empty;
            XDocument? xmlValue = null;
            Exception? xmlException = null;

            if (!String.IsNullOrWhiteSpace(ElementName)) { elementName = ElementName; }
            else if (!String.IsNullOrWhiteSpace(PropertyName)) { elementName = PropertyName; }
            else { elementName = Scope.ToName(); }

            if (value is String strValue)
            { stringValue = strValue; }

            try // Parse the value. Leave as Null if not XML.
            { xmlValue = XDocument.Parse(stringValue); }
            catch (Exception)
            { } // It is not XML

            XElement result;
            if (value is not null)
            {
                if (DataAs is ElementDataAsType.CData && !String.IsNullOrWhiteSpace(stringValue))
                { result = new XElement(elementName, new XCData(stringValue)); }
                else if (DataAs is ElementDataAsType.XML && xmlValue is not null)
                { result = new XElement(elementName, xmlValue); }
                else if (DataAs is ElementDataAsType.Text && AsElement == true)
                { result = new XElement(elementName, value); }
                else if (DataAs is ElementDataAsType.Text && AsAttribute == true)
                { result = new XElement(elementName, new XAttribute("data", value)); }
                else { result = new XElement(elementName); }
            }
            else
            { result = new XElement(elementName); }

            if (!String.IsNullOrWhiteSpace(ElementType))
            { result.Add(new XAttribute("type", ElementType)); }

            if (xmlException is not null)
            { result.Add(new XAttribute("exception", xmlException.Message)); }

            return result;
        }
    }
}
