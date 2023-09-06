using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyItem : IPropertyKey, IPropertyKeyUnique
    { }

    [Serializable]
    public class PropertyItem : BindingTableRow, IPropertyItem, ISerializable
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyTitle { get { return GetValue("PropertyTitle"); } set { SetValue("PropertyTitle", value); } }
        public String? PropertyDescription { get { return GetValue("PropertyDescription"); } set { SetValue("PropertyDescription", value); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } set { SetValue("PropertyName", value); } }
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("Obsolete", value); } }

        public PropertyItem() : base()
        {
            PropertyId = Guid.NewGuid();
            PropertyTitle = "new Property";
            Obsolete = false;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("PropertyTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("PropertyDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null)); }

        static Command GetData(IConnection connection, (Guid? PropertyId, String? PropertyTitle, String? PropertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationProperty]";

            command.AddParameter("@PropertyId", parameters.PropertyId);
            command.AddParameter("@PropertyTitle", parameters.PropertyTitle);
            command.AddParameter("@PropertyName", parameters.PropertyName);
            return command;
        }

        public static Command SetData(IConnection connection, IBindingTable<PropertyItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationProperty]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationProperty]", source);
            return command;
        }

        #region ISerializable
        protected PropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
