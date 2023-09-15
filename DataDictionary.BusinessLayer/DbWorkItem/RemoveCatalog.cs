using DataDictionary.DataLayer.DatabaseData.Catalog;
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
        where TDbItem : class, IDbCatalogKeyUnique, IBindingTableRow
    {
        public required DbCatalogKeyUnique Catalog { get; set; }
        public required IBindingTable<TDbItem> Target { get; set; }

        public RemoveCatalog() : base() { }

        protected override void Work()
        {
            base.Work();
            IEnumerable<TDbItem> toRemove = Target.Where(w => Catalog == new DbCatalogKeyUnique(w)).ToList();

            foreach (TDbItem item in toRemove)
            { Target.Remove(item); }
        }
    }
}
