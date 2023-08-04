using DataDictionary.BusinessLayer.DbWorkItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;
using Toolbox.DbContext;
using DataDictionary.DataLayer.DbMetaData;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// This Database Context is used to get Schema information from the database. 
    /// It is expected to be used Read-Only
    /// </summary>
    public class DbSchemaContext : Toolbox.DbContext.Context, IDbCatalogKeyUnique
    {
        public String? CatalogName { get { return this.DatabaseName; } }
    }
}
