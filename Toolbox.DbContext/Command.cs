using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.DbContext
{
    public class Command
    {
        internal SqlCommand BaseCommand { get; private set; }
        public SqlParameterCollection Parameters { get { return BaseCommand.Parameters; } }
        public String CommandText { get { return BaseCommand.CommandText; } set { BaseCommand.CommandText = value; } }
        public CommandType CommandType { get { return BaseCommand.CommandType; } set { BaseCommand.CommandType = value; } }

        internal Command(SqlCommand command)
        { BaseCommand = command; }
    }
}
