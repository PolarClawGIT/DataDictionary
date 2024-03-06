using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.Tests
{
    [TestClass()]
    public class NameSpaceKeyTests
    {
        [TestMethod()]
        public void NameParts_NewTest()
        {
            List<(String Value, String Expected)> data = new List<(string Value, string Expected)>()
            {
                new ("Test","[Test]"),
                new ("[Test]","[Test]"),
                new ("[Test","[Test]"),
                new ("[Test]]","[Test]"),
                new ("Test]]","[Test]"),
                new ("[[Test.]","[Test]"),
                new ("Test.Child","[Test].[Child]"),
                new ("Test.[Child]","[Test].[Child]"),
                new ("Test.Child]","[Test].[Child]"),
                new ("Test.Child[","[Test].[Child]"),
                new ("Test.[Child[","[Test].[Child]"),
                new ("Test.[Child","[Test].[Child]"),
                new ("Test.Child]][[]","[Test].[Child]"),
                new ("Test.Child[][]","[Test].[Child]"),
                new ("Test{}{})().Child","[Test].[Child]"),
                new ("[Test.Child]","[Test.Child]"),
                
            };


            foreach (var item in data)
            {
                List<string> result = DataLayer.NameSpaceKey.NameParts(item.Value);
                String formated = string.Join(".", result.Select(s => string.Format("[{0}]", s)));

                Assert.AreEqual(item.Expected, formated);
            }
        }
    }
}