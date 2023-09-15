using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface for Database Extended Properties
    /// </summary>
    public interface IDbExtendedProperty
    {  // DB Classes that have extended properties.

        /// <summary>
        /// Returns the Command that Loads the Database Extended properties.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        Command PropertyCommand(IConnection connection);
    }

    internal class DbExtendedPropertyGetCommand : DbExtendedPropertyParameter
    {
        public DbCatalogScope CatalogScope
        {
            get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); }
            set { Level0Type = value.GetScope(); }
        }

        public DbObjectScope ObjectScope
        {
            get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); }
            set { Level1Type = value.GetScope(); }
        }

        public DbElementScope ItemScope
        {
            get { return ExtendedPropertyExtension.GetItemScope(Level2Type); }
            set { Level2Type = value.GetScope(); }
        }

        readonly Command command;

        public DbExtendedPropertyGetCommand(IConnection connection) : base()
        { command = connection.CreateCommand(); }

        public Command GetCommand()
        {
            // There appears to be a bug in Microsoft Code that can cause the parameters to be incorrectly setup when building the SQL statement for the parameters.
            // This appears to be avoid when the parameter is setup with a defined type and length.
            // There is also no way to pass the parameter "Default". For this function, passing NULL is appropriate but this is not always the case.
            command.CommandText = DbScript.DbExtendedPropertyItem;
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
}
