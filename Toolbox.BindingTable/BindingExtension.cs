using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public static class BindingExtension
    {
        public static void AddRange<T>(this BindingList<T> target, IEnumerable<T> source)
        {
            foreach (T item in source)
            { target.Add(item); }
        }
    }
}
