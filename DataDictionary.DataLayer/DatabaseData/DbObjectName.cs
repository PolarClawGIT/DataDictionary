using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    static class  DbObjectName
    {
        /// <summary>
        /// Used to Format a Database Object Name.
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static String Format(params String[] parts)
        {
            String result = String.Empty;

            // Build name
            foreach (String? item in parts)
            {
                if (String.IsNullOrWhiteSpace(item)) { break; }

                if (String.IsNullOrWhiteSpace(result)) { result = String.Format("[{0}]", item); }
                else { result = String.Format("{0}.[{1}]", result, item); }
            }

            return result;
        }
    }
}
