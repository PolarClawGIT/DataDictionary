using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    class LoadExtendedProperties<TDbItem> : WorkItem
        where TDbItem : class, IBindingTableRow, IDbExtendedProperty
    {
        public required BindingTable<DbExtendedPropertyItem> Target { get; init; }
        public required IBindingTable<TDbItem> Source { get; init; }
        public required IConnection Connection { get; init; }

        public LoadExtendedProperties() : base()
        { DoWork = Work; }

        protected void Work()
        {
            Int32 toDo = Source.Count();
            Int32 complete = 0;

            if (Connection is IConnection)
            {
                foreach (TDbItem item in Source)
                {
                    Command command = item.PropertyCommand(Connection);
                    try { Target.Load(Connection.ExecuteReader(command)); }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Command", "MSSQL Extended Property");
                        foreach (DbParameter parameter in command.Parameters)
                        {
                            if (parameter.Value is not null)
                            { ex.Data.Add(parameter.ParameterName, parameter.Value.ToString()); }
                            else { ex.Data.Add(parameter.ParameterName, "(Null)"); }
                        }

                        throw;
                    }

                    complete++;
                    Double progress = ((Double)complete / (Double)toDo) * (Double)100.0;
                    this.OnProgressChanged((Int32)progress);
                }
            }
            else { throw new ArgumentNullException(nameof(Connection)); }
        }
    }
}
