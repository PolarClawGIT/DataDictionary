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
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("Obsolete", value); } }

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

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? helpId, String? helpSubject, String? nameSpace, Boolean? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationHelp]";

            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@HelpSubject", parameters.helpSubject);
            command.AddParameter("@NameSpace", parameters.nameSpace);
            command.AddParameter("@Obsolete", parameters.obsolete);
            return command;
        }

        public static Command SetData(IConnection connection, IBindingTable<HelpItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationHelp]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationHelp]", source);
            return command;
        }
    }

    public static class HelpItemExtension
    {
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, String nameSpace)
        { return source.FirstOrDefault(w => String.Equals(w.NameSpace, nameSpace, ModelFactory.CompareString)); }

    }

}
