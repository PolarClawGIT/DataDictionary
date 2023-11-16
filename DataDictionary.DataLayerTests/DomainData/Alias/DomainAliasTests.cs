using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDictionary.DataLayer.DomainData.Alias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias.Tests
{
    [TestClass()]
    public class DomainAliasTests
    {
        List<String> testData = new List<String>()
            {
                "UnitTest.DataDictionary.App_DataDictionary.Help.[Help.Id]",
                "UnitTest.DataDictionary.[App.DataDictionary].Help.[Help.Id]",
                "UnitTest.DataDictionary.App_DataDictionary.Help.HelpId",
                "UnitTest.[DataDictionary].[App_DataDictionary].[Help].[HelpId]",
                "UnitTest.DataDictionary.App_DataDictionary",
                "UnitTest.DataDictionary.[App_DataDictionary].Help.[HelpId]",
                "UnitTest.[DataDictionary.App_DataDictionary].Help.HelpId",
                "UnitTest.DataDictionary.App_DataDictionary.Help.HelpId",
                "UnitTest.[DataDictionary].[App_DataDictionary].[Help].[HelpId]",
            };

        List<String> expectedData = new List<String>()
        {
                "[UnitTest]",
                "[UnitTest].[DataDictionary]",
                "[UnitTest].[DataDictionary.App_DataDictionary]",
                "[UnitTest].[DataDictionary.App_DataDictionary].[Help]",
                "[UnitTest].[DataDictionary.App_DataDictionary].[Help].[HelpId]",
                "[UnitTest].[DataDictionary].[App_DataDictionary]",
                "[UnitTest].[DataDictionary].[App.DataDictionary]",
                "[UnitTest].[DataDictionary].[App.DataDictionary].[Help]",
                "[UnitTest].[DataDictionary].[App.DataDictionary].[Help].[Help.Id]",
                "[UnitTest].[DataDictionary].[App_DataDictionary].[Help]",
                "[UnitTest].[DataDictionary].[App_DataDictionary].[Help].[HelpId]",
                "[UnitTest].[DataDictionary].[App_DataDictionary].[Help].[Help.Id]",
        };

        [TestMethod()]
        public void ParseNameTest()
        {
            List<String> domainAliases = new List<String>();

            List <String> result = new List<string>();
            foreach (string item in testData)
            { result = result.Union(DomainAlias.ParseName(item)).ToList(); }

            result.Sort();
            Assert.IsTrue(result.Except(expectedData).Count() == 0);
        }
    }
}