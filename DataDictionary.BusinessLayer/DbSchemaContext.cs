using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// This Database Context is used to get Schema information from the database. 
    /// It is expected to be used Read-Only
    /// </summary>
    public class DbSchemaContext : Toolbox.DbContext.Context
    { }
}
