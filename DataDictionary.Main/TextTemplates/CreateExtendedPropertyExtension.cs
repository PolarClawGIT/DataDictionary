using DataDictionary.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.TextTemplates
{
    partial class CreateExtendedProperty
    {
        private ModelData data;

        public CreateExtendedProperty(ModelData data)
        {
            this.data = data;
        }

        void TestCode ()
        {
            foreach (DataLayer.DomainData.DomainAttributeItem item in this.data.DomainAttributes)
            {
                
            }
        }
    }
}
