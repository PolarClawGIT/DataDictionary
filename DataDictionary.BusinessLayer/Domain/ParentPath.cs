using DataDictionary.BusinessLayer.NamedScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Return type used for building Named Scope paths.
    /// </summary>
    struct ParentPath
    {
        public DataLayerIndex ParentKey { get; init; }
        public NamedScopePath Path { get; init; }

        public ParentPath(DataLayerIndex itemParent, NamedScopePath itemPath)
        {
            ParentKey = itemParent;
            Path = itemPath;
        }
    }
}
