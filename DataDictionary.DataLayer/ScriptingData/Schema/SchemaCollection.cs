using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{

    /// <summary>
    /// Generic Base class for Scripting Schema Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SchemaCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISchemaKey>,
        IWriteData, IWriteData<ISchemaKey>,
        IRemoveItem<ISchemaKey>
        where TItem : BindingTableRow, ISchemaItem, ISchemaKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISchemaKey key)
        { return LoadCommand(connection, (key.SchemaId, null)); }

        Command LoadCommand(IConnection connection, (Guid? SchemaId, String? SchemaTitle) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingSchema]";

            command.AddParameter("@SchemaId", parameters.SchemaId);
            command.AddParameter("@SchemaTitle", parameters.SchemaTitle);
            return command;
        }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISchemaKey key)
        { return SaveCommand(connection, (null, key.SchemaId)); }


        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? schemaId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingSchema]";
            command.AddParameter("@SchemaId", parameters.schemaId);

            IEnumerable<TItem> data = this.Where(w => parameters.schemaId is null || w.SchemaId == parameters.schemaId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingSchema]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISchemaKey schemaKey)
        {
            SchemaKey key = new SchemaKey(schemaKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

    }

    /// <summary>
    /// Default List/Collection of Scripting Schema Items.
    /// </summary>
    public class SchemaCollection : SchemaCollection<SchemaItem>
    { }
}
