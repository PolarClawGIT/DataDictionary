﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    class LoadBindingTable : WorkItem, IDbWorkItem
    {
        public required IBindingTable Target { get; init; }
        public required Func<IConnection, IDataReader> Reader { get; init; }
        readonly IConnection connection;

        //public override required string WorkName { get; init; }

        public LoadBindingTable(OpenConnection conn) : base()
        {
            this.connection = conn.Connection;
            conn.Dependency(this);
        }

        protected override void Work()
        { Target.Load(Reader(connection)); }
    }
}