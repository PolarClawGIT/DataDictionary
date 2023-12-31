﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    [Obsolete("Replace with Remove by CatalogKey", true)]
    class RemoveCatalog<TDbItem> : WorkItem
        where TDbItem : class, IDbCatalogKeyName, IBindingTableRow
    {
        public required DbCatalogKeyName Catalog { get; set; }
        public required IBindingTable<TDbItem> Target { get; set; }

        public RemoveCatalog() : base() 
        {   DoWork = Work; }

        void Work()
        {
            IEnumerable<TDbItem> toRemove = Target.Where(w => Catalog == new DbCatalogKeyName(w)).ToList();

            foreach (TDbItem item in toRemove)
            { Target.Remove(item); }
        }
    }
}
