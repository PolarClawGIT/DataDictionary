using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolBox.Enumerable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.EnumerableTests;

namespace ToolBox.Enumerable.Tests
{
    [TestClass()]
    public class SortedHierarchyTests
    {
        List<SortedHierarchyDataValue> testValues;
        List<(SortedHierarchyIndex node, SortedHierarchyIndex? parent)> testRelated;

        public SortedHierarchyTests()
        {
            testValues = new List<SortedHierarchyDataValue>()
            { new SortedHierarchyDataValue("A") { ValueData = "Value A" },               // 0
              new SortedHierarchyDataValue("B") { ValueData = "Value B, parent A & E" }, // 1
              new SortedHierarchyDataValue("C") { ValueData = "Value C, parent B" },     // 2
              new SortedHierarchyDataValue("D") { ValueData = "Value D, parent E" },     // 3
              new SortedHierarchyDataValue("E") { ValueData = "Value E, parent A" },     // 4
            };

            testRelated = new List<(SortedHierarchyIndex node, SortedHierarchyIndex? parent)>
            {
                {(testValues[1],testValues[0])},
                {(testValues[1],testValues[4])},
                {(testValues[2],testValues[1])},
                {(testValues[3],testValues[4])},
                {(testValues[4],testValues[0])},
            };
        }

        private void BuildData(SortedHierarchy<SortedHierarchyIndex, SortedHierarchyDataValue> testData)
        {
            foreach (var item in
                testValues.
                GroupJoin(testRelated,
                    value => value,
                    parent => parent.node,
                    (value, parents) => new { value, parents }).
                SelectMany(s => s.parents, (value, parent) => new { value.value, parent.parent }).
                ToList())
            { testData.Add(item.value, item.value, item.parent); }
        }

        [TestMethod()]
        public void AddTestSimple()
        {
            SortedHierarchy<SortedHierarchyIndex, SortedHierarchyDataValue> testData = new SortedHierarchy<SortedHierarchyIndex, SortedHierarchyDataValue>();

            foreach (var item in testValues)
            { testData.Add(item, item); }

            Assert.IsTrue(testValues.All(w => testData.ContainsValue(w)));

        }

        [TestMethod()]
        public void AddTestWithParents()
        {
            SortedHierarchy<SortedHierarchyIndex, SortedHierarchyDataValue> testData = new SortedHierarchy<SortedHierarchyIndex, SortedHierarchyDataValue>();

            BuildData(testData);

            Assert.IsTrue(testValues.All(w => testData.ContainsValue(w)));
        }


    }
}