using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
{

    public interface IModelItem : IModelIdentifier
    {
        String? ModelTitle { get; set; }
        String? ModelDescription { get; set; }
    }

    public class ModelItem : BindingTableRow, IModelItem
    {
        public Nullable<Guid> ModelId { get { return GetValue<Guid>("ModelId"); } protected set { SetValue<Guid>("ModelId", value); } }
        public String? ModelTitle { get { return GetValue("ModelTitle"); } set { SetValue("ModelTitle", value); } }
        public String? ModelDescription { get { return GetValue("ModelDescription"); } set { SetValue("ModelDescription", value); } }

        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("ModelId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ModelTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("ModelDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection, IModelIdentifier modelIdentifier)
        { return GetData(connection, (modelIdentifier.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? modelTitle, Boolean? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationModel]";

            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@ModelTitle", parameters.modelTitle);
            command.AddParameter("@Obsolete", parameters.obsolete);

            return command;
        }

        public Command SetData(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationModel]";

            command.AddParameter("@ModelId", ModelId);
            command.AddParameter("@ModelTitle", ModelTitle);
            command.AddParameter("@ModelDescription", ModelDescription);
            command.AddParameter("@Obsolete", GetValue("Obsolete"));
            command.AddParameter("@SysStart", GetValue("SysStart"));

            return command;
        }
    }
}
