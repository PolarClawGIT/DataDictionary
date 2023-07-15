using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    class RemoveCatalog<TDbItem> : WorkItem
        where TDbItem : class, IDbCatalogName, IBindingTableRow
    {
        public required DbCatalogName CatalogName { get; set; }
        public required IBindingTable<TDbItem> Target { get; set; }

        public RemoveCatalog() : base() { }

        protected override void Work()
        {
            base.Work();
            IEnumerable<TDbItem> toRemove = Target.Where(w => CatalogName == w).ToList();

            foreach (TDbItem item in toRemove)
            { Target.Remove(item); }
        }
    }
}
