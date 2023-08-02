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
    public interface IPropertyItem : IPropertyIdentifier, IModelIdentifier
    {
        String? PropertyTitle { get; }
    }

    public class PropertyItem : BindingTableRow, IPropertyItem
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyTitle { get { return GetValue("PropertyTitle"); } set { SetValue("PropertyTitle", value); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } set { SetValue("PropertyName", value); } }
        public Nullable<Guid> ModelId { get { return GetValue<Guid>("ModelId"); } set { SetValue<Guid>("ModelId", value); } }

        public PropertyItem() : base()
        { PropertyId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("PropertyTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("ModelId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = true},
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
