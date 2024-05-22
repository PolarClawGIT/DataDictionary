﻿using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaElementValue : IElementItem, IColumnIndex
    { }

    /// <inheritdoc/>
    public class SchemaElementValue : ElementItem, ISchemaElementValue
    {
        /// <inheritdoc/>
        public SchemaElementValue() : base() { }

        /// <inheritdoc/>
        public SchemaElementValue(ISchemaIndex key) : base(key) { }

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
            else if (!String.IsNullOrWhiteSpace(ColumnName)) { elementName = ColumnName; }
            else { elementName = Scope.ToName(); }

            if (value is String strValue)
            { stringValue = strValue; }

            try // Parse the value. Leave as Null if not XML.
            { xmlValue = XDocument.Parse(stringValue); }
            catch (Exception ex)
            {// It is not XML
                //xmlException = ex; 
            }

            XElement result;
            if (value is not null)
            {
                if (DataAsCData == true && !String.IsNullOrWhiteSpace(stringValue))
                { result = new XElement(elementName, new XCData(stringValue)); }
                else if (DataAsXml == true && xmlValue is not null)
                { result = new XElement(elementName, xmlValue); }
                else if (DataAsText == true && AsElement == true)
                { result = new XElement(elementName, value); }
                else if (DataAsText == true && AsAttribute == true)
                { result = new XElement(elementName, new XAttribute("data", value)); }
                else { result = new XElement(elementName); }
            }
            else
            { result = new XElement(elementName); }

            if (ElementNillable.HasValue)
            { result.Add(new XAttribute("nill", ElementNillable.HasValue)); }

            if (!String.IsNullOrWhiteSpace(ElementType))
            { result.Add(new XAttribute("type", ElementType)); }

            if(xmlException is not null)
            { result.Add(new XAttribute("exception", xmlException.Message)); }

            return result;
        }
    }
}
