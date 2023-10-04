using DataDictionary.DataLayer.ApplicationData.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Generic Base class for Database Constraint Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbConstraintCollection<TItem> : BindingTable<TItem>, IReadData<IModelKey>, IReadSchema, IWriteData<IModelKey>
        where TItem : DbConstraintItem, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbConstraintItem;
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, string? catalogName, string? schemaName, string? constraintName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseConstraint]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ConstraintName", parameters.constraintName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseConstraint]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseConstraint]", this);
            return command;
        }
    }

    /// <summary>
    /// Default List/Collection of Database Constraint Items.
    /// </summary>
    public class DbConstraintCollection : DbConstraintCollection<DbConstraintItem>
    { }
}
