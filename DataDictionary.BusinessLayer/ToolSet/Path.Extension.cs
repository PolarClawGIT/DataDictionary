using DataDictionary.BusinessLayer.AppCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.ToolSet
{
    static class PathExtension
    {
        /// <summary>
        /// Creates a PathIndex for a TableColumnValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static PathIndex CreatePath(this ITableColumnIndexName value)
        { return new PathIndex(PathIndex.Parse(new TableColumnIndexName(value).ToString()).ToArray()); }
    }
}
