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
    public interface IPropertyItem: IModelIdentifier
    {

    }

    public class PropertyItem : BindingTableRow, IPropertyItem
    {
        public Nullable<Int32> PropertyId { get { return GetValue<Int32>("PropertyId"); } }
        public String? PropertyTitle { get { return GetValue("PropertyTitle"); }  }
        public Nullable<Guid> ModelId { get { return GetValue<Guid>("ModelId"); } }

        public PropertyItem() : base()
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Int32)){ AllowDBNull = false},
            new DataColumn("PropertyTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("ModelId", typeof(Guid)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? PropertyId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationProperty]";

            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@PropertyId", parameters.PropertyId);
            return command;
        }


    }
}
