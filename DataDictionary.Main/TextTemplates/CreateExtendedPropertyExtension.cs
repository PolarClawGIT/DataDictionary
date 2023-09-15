using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.TextTemplates
{
    partial class CreateExtendedProperty
    {
        private IEnumerable<DbExtendedPropertyParameter> data;

        public CreateExtendedProperty(IEnumerable<DbExtendedPropertyParameter> data)
        { this.data = data; }

        void TestCode()
        {
            foreach (DbExtendedPropertyParameter item in this.data)
            {
                var x = String.Format(
                    "exec sp_addextendedproperty @name = '{0}', @value = '{1}', @level0type = '{2}', @level0name = '{3}', @level1type = '{4}', @level1name = '{5}', @level2type = '{6}', @level2name = '{7}' ",
                    item.PropertyName, item.PropertyValue,
                    item.Level0Type, item.Level0Name,
                    item.Level1Type, item.Level1Name,
                    item.Level2Type, item.Level2Name);
            }
        }
    }
}
