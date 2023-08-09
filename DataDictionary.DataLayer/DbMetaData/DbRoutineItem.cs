using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbRoutineItem : IDbRoutineKey, IDbCatalogKey, IDbIsSystem, IBindingTableRow
    {
        String? RoutineType { get; }
    }

    public class DbRoutineItem : BindingTableRow, IDbRoutineItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? RoutineName { get { return GetValue("RoutineName"); } }
        public String? RoutineType { get { return GetValue("RoutineType"); } }
        public Boolean IsSystem
        {
            get
            {
                return SchemaName is "dbo" &&
                    RoutineName is "sp_creatediagram" or
                    "sp_renamediagram" or
                    "sp_alterdiagram" or
                    "sp_dropdiagram" or
                    "fn_diagramobjects" or
                    "sp_helpdiagrams" or
                    "sp_helpdiagramdefinition" or
                    "sp_upgraddiagrams";
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineType", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbRoutineItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            {
                Level0Name = SchemaName,
                Level0Type = "SCHEMA",
                Level1Name = RoutineName,
                Level1Type = RoutineType
            }).GetCommand();

        }
    }
}
