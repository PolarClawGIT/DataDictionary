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

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// This Database Context is used to get Schema information from the database. 
    /// It is expected to be used Read-Only
    /// </summary>
    public class DbSchemaContext : Toolbox.DbContext.Context
    {
        public WorkItem VerifyConnection()
        { return new VerifyConnection() { Context = this }; }

        public WorkItem GetDatabaseList(IList<DbCatalogName> target)
        {
            GetInformationSchema<DbCatalogName> item = new GetInformationSchema<DbCatalogName>(this)
            {
                WorkName = "Get Database List",
                Collection = InformationSchema.Collection.Databases,
                Transform = convert,
                Target = target,
                Filter = w => !(w.CatalogName is "master" or "msdb" or "tempdb" or "model")
            };

            return item;

            DbCatalogName convert(DataRow row)
            {
                if (row[0].ToString() is String value)
                { return new DbCatalogName() { CatalogName = value }; }
                else { return new DbCatalogName(); }
            }
        }

    }
}
