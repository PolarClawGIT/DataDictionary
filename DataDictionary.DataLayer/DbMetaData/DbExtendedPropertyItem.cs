using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Microsoft.Data.SqlClient;

namespace DataDictionary.DataLayer.DbMetaData
{

    public interface IDbExtendedPropertyItem
    {
        String? Level0Type { get; }
        String? Level0Name { get; }
        String? Level1Type { get; }
        String? Level1Name { get; }
        String? Level2Type { get; }
        String? Level2Name { get; }

        String? ObjectType { get; }
        String? ObjectName { get; }
        String? PropertyName { get; }
        String? PropertyValue { get; }
    }

    internal class DbExtendedPropertyGetCommand
    {
        public String? PropertyName { get; set; } // Null will return all extended properties
        public String? Level0Type { get; set; }
        public String? Level0Name { get; set; } // Null will return all objects of Level0 matching the Type
        public String? Level1Type { get; set; }
        public String? Level1Name { get; set; } // Null will return all objects of Level1 matching the Type and of Level0 Name
        public String? Level2Type { get; set; }
        public String? Level2Name { get; set; } // Null will return all objects of Level2 matching the Type and of Level0 & Level1 Name

        readonly SqlCommand command;

        public DbExtendedPropertyGetCommand(IConnection connection) : base()
        { command = connection.CreateCommand(); }

        public SqlCommand GetCommand()
        {
            // There appears to be a bug in Microsoft Code that can cause the paramters to be incorrectly setup when building the SQL statement for the parameters.
            // This appears to be avoid when the paramter is setup with a defined type and length.
            // There is also no way to pass the parameter "Default". For this function, passing NULL is apporate but this is not always the case.
            command.CommandText = "SELECT @Level0Type [Level0Type], @Level0Name [Level0Name], @Level1Type [Level1Type], @Level1Name [Level1Name], @Level2Type [Level2Type], @Level2Name [Level2Name], [objtype], [objname], [name], [value] FROM [fn_listextendedproperty](@PropertyName, @Level0Type, @Level0Name, @Level1Type, @Level1Name, @Level2Type, @Level2Name)";
            command.Parameters.Add(new SqlParameter("@PropertyName", SqlDbType.VarChar, 210)); 
            command.Parameters.Add(new SqlParameter("@Level0Type", SqlDbType.VarChar, 210)); 
            command.Parameters.Add(new SqlParameter("@Level0Name", SqlDbType.VarChar, 210)); 
            command.Parameters.Add(new SqlParameter("@Level1Type", SqlDbType.VarChar, 210)); 
            command.Parameters.Add(new SqlParameter("@Level1Name", SqlDbType.VarChar, 210)); 
            command.Parameters.Add(new SqlParameter("@Level2Type", SqlDbType.VarChar, 210)); 
            command.Parameters.Add(new SqlParameter("@Level2Name", SqlDbType.VarChar, 210)); 

            command.Parameters["@PropertyName"].Value = PropertyName is null ? DBNull.Value : PropertyName;
            command.Parameters["@Level0Type"].Value = Level0Type is null ? DBNull.Value : Level0Type;
            command.Parameters["@Level0Name"].Value = Level0Name is null ? DBNull.Value : Level0Name;
            command.Parameters["@Level1Type"].Value = Level1Type is null ? DBNull.Value : Level1Type;
            command.Parameters["@Level1Name"].Value = Level1Name is null ? DBNull.Value : Level1Name;
            command.Parameters["@Level2Type"].Value = Level2Type is null ? DBNull.Value : Level2Type;
            command.Parameters["@Level2Name"].Value = Level2Name is null ? DBNull.Value : Level2Name;

            return command;
        }
    }


    public class DbExtendedPropertyItem : BindingTableRow, IDbExtendedPropertyItem, INotifyPropertyChanged
    {
        public String? Level0Type { get { return GetValue("Level0Type"); } }
        public String? Level0Name { get { return GetValue("Level0Name"); } }
        public String? Level1Type { get { return GetValue("Level1Type"); } }
        public String? Level1Name { get { return GetValue("Level1Name"); } }
        public String? Level2Type { get { return GetValue("Level2Type"); } }
        public String? Level2Name { get { return GetValue("Level2Name"); } }

        public String? ObjectType { get { return GetValue("objtype"); } }
        public String? ObjectName { get { return GetValue("objname"); } }
        public String? PropertyName { get { return GetValue("name"); } }
        public String? PropertyValue { get { return GetValue("value"); } }

    }
}
