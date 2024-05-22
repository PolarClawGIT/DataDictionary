using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Data Object that support XML Scripting
    /// </summary>
    public interface IScripting
    {
        /// <summary>
        /// Returns the data as XElement.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        XElement GetXElement(IEnumerable<SchemaElementValue>? options = null);
    }
}
