using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on TemplateScriptAs Enum. 
    /// </summary>
    public static class TemplateScriptAsExtension
    {
        /// <summary>
        /// Gets the Details for the TemplateScriptAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TemplateScriptAsEnumeration GetEnumeration(this TemplateScriptAsType value)
        { return TemplateScriptAsEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a TemplateScriptAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out TemplateScriptAsType result)
        {
            if (TemplateScriptAsEnumeration.TryParse(value, null, out TemplateScriptAsEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = TemplateScriptAsType.none; return false; }
        }
    }
}
