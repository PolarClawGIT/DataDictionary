using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.EnumerableTests
{
    class SortedHierarchyDataValue : ISortedHierarchyIndex
    {
        public String NameKey { get; init; }
        public String ValueData { get; set; } = String.Empty;

        public SortedHierarchyDataValue(String key)
        { NameKey = key; }

        public static implicit operator SortedHierarchyIndex(SortedHierarchyDataValue source)
        { return new SortedHierarchyIndex(source); }

        public override String ToString()
        { return ValueData; }
    }
}
