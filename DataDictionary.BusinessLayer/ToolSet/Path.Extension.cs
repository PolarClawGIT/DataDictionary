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
        public static PathItem CreatePath(this ITableColumnIndexName value)
        { return new PathItem(PathItem.Parse(new TableColumnIndexName(value).ToString()).ToArray()); }
    }
}
