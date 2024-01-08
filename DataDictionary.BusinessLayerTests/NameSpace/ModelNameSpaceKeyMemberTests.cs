using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDictionary.BusinessLayer.NameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NameSpace.Tests
{
    [TestClass()]
    public class ModelNameSpaceKeyMemberTests
    {
        [TestMethod()]
        public void CompareToTest()
        {
            String base01 = String.Empty;
            String base02 = this.GetType().Name;
            String base03 = String.Format("{0}.Testing", this.GetType().Name);

            if (this.GetType().FullName is String nameValue) { base01 = nameValue; }

            Int32 test01 = new ModelNameSpaceKeyMember(base01).CompareTo(new ModelNameSpaceKeyMember(base01));
            Int32 test02 = new ModelNameSpaceKeyMember(base01).CompareTo(new ModelNameSpaceKeyMember(base02));
            Int32 test03 = new ModelNameSpaceKeyMember(base02).CompareTo(new ModelNameSpaceKeyMember(base01));
            Int32 test04 = new ModelNameSpaceKeyMember(base02).CompareTo(new ModelNameSpaceKeyMember(base03));
            Int32 test05 = new ModelNameSpaceKeyMember(base03).CompareTo(new ModelNameSpaceKeyMember(base02));

            Assert.AreEqual(0, test01);
            Assert.AreEqual(-1, test02);
            Assert.AreEqual(1, test03);

            // base02 is shorter then base03. Common elements are identical.
            Assert.AreEqual(-1, test04); 
            Assert.AreEqual(1, test05);

            Assert.Fail();
        }
    }
}