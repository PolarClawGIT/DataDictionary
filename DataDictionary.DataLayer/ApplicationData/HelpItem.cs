using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IHelpItem
    {
        Nullable<Guid> HelpId { get; }
        Nullable<Guid> HelpParentId { get; }
        String? HelpSubject { get; }
        String? HelpText { get; }
        String? NameSpace { get; }
        Nullable<Boolean> Obsolete { get; }
    }

    public class HelpItem : BindingTableRow, IHelpItem
    {
        public Nullable<Guid> HelpId { get { return GetValue<Guid>("HelpId"); } protected set { SetValue<Guid>("HelpId", value); } }
        public Nullable<Guid> HelpParentId { get { return GetValue<Guid>("HelpParentId"); } protected set { SetValue<Guid>("HelpParentId", value); } }
        public String? HelpSubject { get { return GetValue("HelpSubject"); } set { SetValue("HelpSubject", value); } }
        public String? HelpText { get { return GetValue("HelpText"); } set { SetValue("HelpText", value); } }
        public String? NameSpace { get { return GetValue("NameSpace"); } set { SetValue("NameSpace", value); } }
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryPrase); } set { SetValue<Boolean>("Obsolete", value); } }

        public HelpItem() : base()
        {
            if (HelpId is null) { HelpId = Guid.NewGuid(); }
            if (Obsolete is null) { Obsolete = false; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("HelpId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("HelpParentId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("HelpSubject", typeof(String)){ AllowDBNull = false},
            new DataColumn("HelpText", typeof(String)){ AllowDBNull = false},
            new DataColumn("NameSpace", typeof(String)){ AllowDBNull = false},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static IDataReader GetData(IConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "procGetApplicationHelp";
            return command.ExecuteReader();
        }

        public static SqlCommand SetData(IBindingTable<HelpItem> source, IConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "procSetApplicationHelp";

            SqlParameter dataValues = new SqlParameter("@Data", SqlDbType.Structured);
            dataValues.TypeName = "App_DataDictionary.typeApplicationHelp";
            DataTable data = new DataTable();
            data.Load(source.CreateDataReader());
            dataValues.Value = data;
            command.Parameters.Add(dataValues);
            return command;
        }

    }

    public static class HelpItemExtension
    {
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, String nameSpace)
        { return source.FirstOrDefault(w => String.Equals(w.NameSpace, nameSpace, ModelFactory.CompareString)); }

    }

}
