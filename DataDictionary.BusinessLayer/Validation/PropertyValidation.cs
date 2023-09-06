using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Validation
{
    public static class PropertyValidation
    {
        public static String Validate(this PropertyItem item)
        {
            if (String.IsNullOrEmpty(item.PropertyTitle)) { return "PropertyTitle required"; }

            PropertyKey primaryKey = new PropertyKey(item);
            PropertyKeyUnique uniqueKey = new PropertyKeyUnique(item);

            if (item.BindingTable is IBindingTable<PropertyItem> table
                && table.FirstOrDefault(w => !primaryKey.Equals(w) && uniqueKey.Equals(w))
                is PropertyItem errorItem)
            { return "PropertyTitle must be unique"; }

            return String.Empty;
        }
    }
}
