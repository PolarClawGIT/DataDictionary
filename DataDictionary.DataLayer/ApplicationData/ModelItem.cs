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

    public interface IModelItem : IModelKey
    {
        String? ModelTitle { get; set; }
        String? ModelDescription { get; set; }
    }

    public class ModelItem : BindingTableRow, IModelItem
    {
        public Nullable<Guid> ModelId { get { return GetValue<Guid>("ModelId"); } protected set { SetValue<Guid>("ModelId", value); } }
        public String? ModelTitle { get { return GetValue("ModelTitle"); } set { SetValue("ModelTitle", value); } }
        public String? ModelDescription { get { return GetValue("ModelDescription"); } set { SetValue("ModelDescription", value); } }
        public Boolean? Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue("Obsolete", value); } }

        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
            Obsolete = false;
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

        public static Command GetData(IConnection connection, IModelKey modelIdentifier)
        { return GetData(connection, (modelIdentifier.ModelId, null, true)); }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, true)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? modelTitle, Boolean? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetModel]";

            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@ModelTitle", parameters.modelTitle);
            command.AddParameter("@Obsolete", parameters.obsolete);

            return command;
        }

        public static Command SetData(IConnection connection, IBindingTable<ModelItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModel]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeModel]", source);

            return command;
        }

        public static Command DeleteData (IConnection connection, IModelKey parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procDeleteModel]";
            command.AddParameter("@ModelId", parameters.ModelId);

            return command;
        }
    }
}
