using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// List of Value Types for an Object Property.<br/>
    /// </summary>
    /// <remarks>
    /// This is used instead of PropertyInfo.PropertyType to assign a set of behavior based on type.<br/>
    /// Assigns a Icon/Image to a property.
    /// </remarks>
    public enum ObjectValueType
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
        /// Something used to define a namespace like value such as an alias name, object path, or a qualified name.
        /// </summary>
        NameSpace,

        /// <summary>
        /// Generic Enumeration value
        /// </summary>
        Enumeration,

        // Note needed? //

        /// <summary>
        /// Generic Class value
        /// </summary>
        Class,

        /// <summary>
        /// Generic Structure value
        /// </summary>
        Structure,

        /// <summary>
        /// Generic Record value
        /// </summary>
        Record,

        /// <summary>
        /// Generic Interface value
        /// </summary>
        Interface,

    }

}
