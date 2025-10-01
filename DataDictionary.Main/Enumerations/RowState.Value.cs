using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    static partial class RowStateExtension
    {
        partial class Enumeration
        {
            static Enumeration()
            {
                List<Enumeration> data = new List<Enumeration>()
            {
                new Enumeration(BindingRowState.Null,      Resources.Icon_Row.GetSmallImage()),
                new Enumeration(BindingRowState.Detached,  Resources.Icon_Row.MergeImage(Resources.ItemDetached)),
                new Enumeration(BindingRowState.Unchanged, Resources.Icon_Row.GetSmallImage()),
                new Enumeration(BindingRowState.Added,     Resources.Icon_Row.MergeImage(Resources.ItemAdded)),
                new Enumeration(BindingRowState.Deleted,   Resources.Icon_Row.MergeImage(Resources.ItemDelete)),
                new Enumeration(BindingRowState.Modified,  Resources.Icon_Row.MergeImage(Resources.ItemModified)),
                new Enumeration(BindingRowState.Historic,  Resources.Icon_Row.MergeImage(Resources.ItemHistory)),
            };

                BuildDictionary(data);
            }
        }
    }
}
