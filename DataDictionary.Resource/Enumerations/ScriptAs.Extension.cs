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
    public static class ScriptAsExtension
    {
        /// <summary>
        /// Gets the Details for the TemplateScriptAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<ScriptAsType> GetEnumeration(this ScriptAsType value)
        { return ScriptAsEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a TemplateScriptAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out ScriptAsType result)
        {
            if (ScriptAsEnumeration.TryParse(value, null, out ScriptAsEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = ScriptAsType.none; return false; }
        }
    }
}
