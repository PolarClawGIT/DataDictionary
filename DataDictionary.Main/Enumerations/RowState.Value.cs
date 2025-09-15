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
                new Enumeration(BindingRowState.Null,      Resources.Row),
                new Enumeration(BindingRowState.Detached,  Resources.RowDetached),
                new Enumeration(BindingRowState.Unchanged, Resources.Row),
                new Enumeration(BindingRowState.Added,     Resources.RowAdded),
                new Enumeration(BindingRowState.Deleted,   Resources.RowDeleted),
                new Enumeration(BindingRowState.Modified,  Resources.RowModified),
                new Enumeration(BindingRowState.Historic,  Resources.RowHistory),
            };

                BuildDictionary(data);
            }
        }
    }
}
