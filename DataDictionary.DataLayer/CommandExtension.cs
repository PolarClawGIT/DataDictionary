using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer
{
    static class CommandExtension
    {
        /// <summary>
        /// Conditionally adds the parameter to the command
        /// Allows data be passed to the Db using Guid.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AddParameter(this Command command, String parameterName, Guid? value)
        {
            if (value is not null)
            {
                command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.UniqueIdentifier)
                { Value = value });
            }
        }

        /// <summary>
        /// Conditionally adds the parameter to the command
        /// Allows data be passed to the Db using Int32.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AddParameter(this Command command, String parameterName, Int32? value)
        {
            if (value is not null)
            {
                command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.Int)
                { Value = value });
            }
        }

        /// <summary>
        /// Conditionally adds the parameter to the command.
        /// Allows data be passed to the Db using String.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AddParameter(this Command command, String parameterName, String? value)
        {
            if (value is not null && !String.IsNullOrWhiteSpace(value))
            {
                command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.NVarChar)
                { Value = value });
            }
        }

        /// <summary>
        /// Conditionally adds the parameter to the command.
        /// Allows data be passed to the Db using Boolean.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AddParameter(this Command command, String parameterName, Boolean? value)
        {
            if (value is not null)
            {
                command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.NVarChar)
                { Value = value });
            }
        }

        /// <summary>
        /// Conditionally adds the parameter to the command.
        /// Allows data be passed to the Db using a SQL Table Type.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="structureType"></param>
        /// <param name="value"></param>
        /// <remarks>Handles a Enumeration of BindingTableRow</remarks>
        public static void AddParameter<T>(this Command command, String parameterName, String structureType, IEnumerable<T> value)
            where T : BindingTableRow, new()
        {
            if (value is not null)
            {
                DataTable data = value.ToDataTable();
                command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.Structured)
                { Value = data, TypeName = structureType });
            }
        }

    }
}
