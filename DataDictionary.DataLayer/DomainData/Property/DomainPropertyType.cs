using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Property
{

    /// <summary>
    /// List of supported Table Types.
    /// </summary>
    public enum DomainPropertyType
    {
        /// <summary>
        /// Unknown Type
        /// </summary>
        Null,

        /// <summary>
        /// Contains String Data
        /// </summary>
        String,

        /// <summary>
        /// Contains Integer Data
        /// </summary>
        Integer,

        /// <summary>
        /// Contains a List of Comma Separated values
        /// </summary>
        /// <remarks>Data element contains valid list values.</remarks>
        List,

        /// <summary>
        /// Contains XML
        /// </summary>
        Xml,

        /// <summary>
        /// Contains an MS Extended Property.
        /// </summary>
        /// <remarks>This needs special handling. Data Element contains the Extended Property Name.</remarks>
        MS_ExtendedProperty
    }

    /// <summary>
    /// Support Extension for the Scope Type
    /// </summary>
    public static class DomainPropertyTypeExtension
    {
        /// <summary>
        /// Translates the PropertyType to a Property DataType (String).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToName(this DomainPropertyType value)
        { return new DomainPropertyTypeKey(value).ToString(); }
    }

}
