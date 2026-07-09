using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// List of Object Property Types.
    /// </summary>
    /// <remarks>This is to give an Icon/Image to a property.</remarks>
    public enum ObjectPropertyType
    {
        // Future: Help define behaviors, especially when working with XML generation.

        /// <summary>
        /// Represents the undefined Object Type.
        /// </summary>
        Null,

        /// <summary>
        /// Generic String value.
        /// </summary>
        String,

        /// <summary>
        /// An Enumeration value represented as a String.
        /// </summary>
        StringEnumeration,

        /// <summary>
        /// XML value represented as a String.
        /// </summary>
        StringXML,

        /// <summary>
        /// Rich Text value represented as a String.
        /// </summary>
        StringRichText,

        /// <summary>
        /// List of delimited values represented as a String.
        /// </summary>
        StringList,

        /// <summary>
        /// Generic Numeric value, including integers and real numbers.
        /// </summary>
        Numeric,

        /// <summary>
        /// Generic Boolean value. True/False
        /// </summary>
        Boolean,

        /// <summary>
        /// Generic Date/Time value
        /// </summary>
        DateTime,

        /// <summary>
        /// Globally Unique Identifier
        /// </summary>
        GUID,

        /// <summary>
        /// Generic Class value, an un-defined class type
        /// </summary>
        Class,

        /// <summary>
        /// Something used to define a namespace like value such as an alias name, object path, or a qualified name.
        /// </summary>
        NameSpace,
    }

}
