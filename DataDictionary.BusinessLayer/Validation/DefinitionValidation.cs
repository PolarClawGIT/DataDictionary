using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Validation
{
    public static class DefinitionValidation
    {

        public static String Validate(this DefinitionItem item)
        {
            if (String.IsNullOrEmpty(item.DefinitionTitle)) { return "DefinitionTitle required"; }

            DefinitionKey primaryKey = new DefinitionKey(item);
            DefinitionKeyUnique uniqueKey = new DefinitionKeyUnique(item);

            if(item.BindingTable is IBindingTable<DefinitionItem> x)
            {
                var y = x.FirstOrDefault(w => uniqueKey.Equals(w));
                var z = x.FirstOrDefault(w => !primaryKey.Equals(w));
            }

            if (item.BindingTable is IBindingTable<DefinitionItem> table
                && table.FirstOrDefault(w => !primaryKey.Equals(w) && uniqueKey.Equals(w))
                is DefinitionItem errorItem)
            { return "DefinitionTitle must be unique"; }

            return String.Empty;
        }
    }
}
