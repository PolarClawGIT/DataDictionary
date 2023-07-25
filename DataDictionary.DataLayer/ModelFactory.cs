using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer
{
    public static class ModelFactory
    {
        public static StringComparison CompareString { get; } = StringComparison.CurrentCultureIgnoreCase;

        /// <summary>
        /// Helper method to create BindingTables with the default initialization state.
        /// </summary>
        /// <typeparam name="TBinding"></typeparam>
        /// <returns></returns>
        public static BindingTable<TBinding> Create<TBinding>()
            where TBinding : BindingTableRow, INotifyPropertyChanged, IBindingTableRow, new()
        {
            return new BindingTable<TBinding>() { CompareString = ModelFactory.CompareString };
        }
    }

    static class ModelFactoryInternal
    {
        /// <summary>
        /// Conditionally adds the parameter to the command
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
        /// Conditionally adds the parameter to the command
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
        /// Conditionally adds the parameter to the command
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
        /// Conditionally adds the parameter to the command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="structureType"></param>
        /// <param name="value"></param>
        public static void AddParameter(this Command command, String parameterName, String structureType, IBindingTable value)
        {
            if (value is not null)
            {
                using (DataTable data = new DataTable())
                {
                    data.Load(value.CreateDataReader());

                    command.Parameters.Add(new SqlParameter(parameterName, SqlDbType.Structured)
                    { Value = data, TypeName = structureType });
                }
            }
        }

    }
}
